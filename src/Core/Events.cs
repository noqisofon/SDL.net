// 
//  Events.cs
//  
//  Author:
//       ned rihine <ned.rihine@gmail.com>
//  
//  Copyright (c) 2012 ned rihine All rights reserved.
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Resources;


namespace Sdl.Core {

	
	using Sdl.Audio;
	using Sdl.Graphics;
	using Sdl.Input;	

	
	/// <summary>
	/// Events.
	/// </summary>
	/// <exception cref='ArgumentNullException'>
	/// Is thrown when an argument passed to a method is invalid because it is <see langword="null" /> .
	/// </exception>
    public sealed class Events {
#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.Events"/> class.
		/// </summary>
        Events() {}
#endregion


#region Public Events
        public static event EventHandler<ActiveEventArgs> Active;

        public static event EventHandler<KeyboardEventArgs> KeyboardDown;
        public static event EventHandler<KeyboardEventArgs> KeyboardUp;

        public static event EventHandler<MouseMotionEventArgs> MouseMotion;
        public static event EventHandler<MouseButtonEventArgs> MouseButtonDown;
        public static event EventHandler<MouseButtonEventArgs> MouseButtonUp;

        public static event EventHandler<JoystickButtonEventArgs> JoystickButtonDown;
        public static event EventHandler<JoystickButtonEventArgs> JoystickButtonUp;
        public static event EventHandler<JoystickAxisEventArgs> JoystickAxisMotion;
        public static event EventHandler<JoystickHatEventArgs> JoystickHatMotion;
        public static event EventHandler<JoystickBallEventArgs> JoystickBallMotion;

        public static event EventHandler<VideoResizeEventArgs> VideoResize;
        public static event EventHandler<VideoExposeEventArgs> VideoExpose;

        public static event EventHandler<QuitEventArgs> Quit;
        public static event EventHandler<WindowManagerEventArgs> WindowManagerEvent;

        public static event EventHandler<ChannelFinishedEventArgs> ChannelFinished;
        public static event EventHandler<MusicFinishedEventArgs> MusicFinished;
		public static event EventHandler<UserEventArgs> UserEvent;

        public static event EventHandler<TickEventArgs> Tick;
#endregion


#region Public Properties
        public static ResourceManager StringManager {
            get { return __string_manager; }
            set { __string_manager = value; }
        }


        public static int Fps {
            get { return __fps; }
            set { __target_fps = value; }
        }


        public static int TargetFps {
            get { return __target_fps; }
            set {
                if ( value == 0 )
                    __target_fps = 1;
                else if ( value > 100000 || value == -1 )
                    __target_fps = 100000;
                else
                    __target_fps = value;
				__ticks_per_frame = 1000.0f / (float)__target_fps;
            }
        }
#endregion


#region Public Methods
        public static bool Poll() {
            try {
                SdlSystem.SDL_Event e;
                int ret = SdlSystem.SDL_PollEvent( out e );

                if ( ret == (int)NativeFunctionReturnFlags.Error )
                    throw SdlException.Generate();
                if ( ret == (int)NativeFunctionReturnFlags.None )
                    return false;
                else {
                    ProcessEvent( e );

                    return true;
                }
            } catch ( AccessViolationException ) {
                return false;
            }
        }
        public static bool Poll(int number_of_events) {
            SdlSystem.SDL_Event e;
            bool ret = false;

            for ( int i = 0; i < number_of_events; ++ i ) {
                int retval = SdlSystem.SDL_PollEvent( out e );

                if ( retval == (int)NativeFunctionReturnFlags.Error )
                    throw SdlException.Generate();
                if ( retval == (int)NativeFunctionReturnFlags.None ) {
                    ret = true;

                    break;
                } else
                    ProcessEvent( e );
            }
			return ret;
        }


        public static void Wait() {
            SdlSystem.SDL_Event e;

            if ( SdlSystem.SDL_WaitEvent( out e ) == (int)NativeFunctionReturnFlags.Error )
                throw SdlException.Generate();

            ProcessEvent( e );
        }


        public static void PushUserEvent(UserEventArgs user_event) {
            if ( null == user_event )
                throw new ArgumentNullException( "user_event" );

            lock ( __instance ) {
                __user_events[__user_event_id.ToString()] = user_event;
                user_event.UserCode = __user_event_id;
                ++ __user_event_id;
            }

            SdlSystem.SDL_Event e = user_event.EventStruct;
            if ( SdlSystem.SDL_PushEvent( out e ) != (int)NativeFunctionReturnFlags.Success ) {
                /* nothing */
            }
        }


        public static void PushEvent(SdlEventArgs sdl_event) {
            if ( null == sdl_event )
                throw new ArgumentNullException( "sdl_event" );

            SdlSystem.SDL_Event e = sdl_event.EventStruct;
            if ( SdlSystem.SDL_PushEvent( out e ) != (int)NativeFunctionReturnFlags.Success ) {
                /* nothing */
            }
        }


        public static void PushAllEvent(SdlEventArgs[] sdl_events) {
            if ( null == sdl_events )
                throw new ArgumentNullException( "sdl_events" );

            foreach ( SdlEventArgs sdl_event in sdl_events ) {
                PushEvent( sdl_event );
            }
        }


        public static void Remove() {
            Remove( EventMask.AllEvents, QUERY_EVENTS_MAX );
        }
        public static void Remove(EventMask event_mask) {
            Remove( event_mask, QUERY_EVENTS_MAX );
        }
        public static void Remove(int number_of_events) {
            Remove( EventMask.AllEvents, number_of_events );
        }
        public static void Remove(EventMask event_mask, int number_of_events) {
            SdlSystem.SDL_Event[] events = new SdlSystem.SDL_Event[number_of_events];

            SdlSystem.SDL_PumpEvents();

            int ret = SdlSystem.SDL_PeepEvents( out events,
                                                events.Length,
                                                SdlSystem.SDL_eventaction.SDL_GETEVENT,
                                                (uint)event_mask );
            if ( ret == (int)NativeFunctionReturnFlags.Error )
                throw SdlException.Generate();
        }


        public static void RemoveAllEvent() {
            Remove( EventMask.AllEvents, QUERY_EVENTS_MAX );
        }


        public static SdlEventArgs[] Retrieve() {
            return Retrieve( EventMask.AllEvents, QUERY_EVENTS_MAX );
        }
        public static SdlEventArgs[] Retrieve(EventMask event_mask) {
            return Retrieve( event_mask, QUERY_EVENTS_MAX );
        }
        public static SdlEventArgs[] Retrieve(int number_of_events) {
            return Retrieve( EventMask.AllEvents, number_of_events );
        }
        public static SdlEventArgs[] Retrieve(EventMask event_mask, int number_of_events) {
            SdlSystem.SDL_PumpEvents();

            SdlSystem.SDL_Event[] events = new SdlSystem.SDL_Event[number_of_events];

            int ret = SdlSystem.SDL_PeepEvents( out events,
                                                events.Length,
                                                SdlSystem.SDL_eventaction.SDL_GETEVENT,
                                                (uint)event_mask );
            if ( ret == (int)NativeFunctionReturnFlags.Error )
                throw SdlException.Generate();

            SdlEventArgs[] eventargs_array = new SdlEventArgs[ret];
            for ( int i = 0; i < eventargs_array.Length; ++ i ) {
                if ( events[i].type == (byte)EventTypes.UserEvent ) {
                    eventargs_array[i] = __user_events[events[i].user.code.ToString()];
                    __user_events.Remove( events[i].user.code.ToString() );
                } else
                    eventargs_array[i] = SdlEventArgs.CreateEventArgs( events[i] );
            }
            return eventargs_array;
        }


        public static SdlEventArgs[] Peek() {
            return Peek( EventMask.AllEvents, QUERY_EVENTS_MAX );
        }
        public static SdlEventArgs[] Peek(EventMask event_mask) {
            return Peek( event_mask, QUERY_EVENTS_MAX );
        }
        public static SdlEventArgs[] Peek(int number_of_events) {
            return Peek( EventMask.AllEvents, number_of_events );
        }
        public static SdlEventArgs[] Peek(EventMask event_mask, int number_of_events) {
            SdlSystem.SDL_Event[] events = new SdlSystem.SDL_Event[number_of_events];

            SdlSystem.SDL_PumpEvents();

            int ret = SdlSystem.SDL_PeepEvents( out events,
                                                events.Length,
                                                SdlSystem.SDL_eventaction.SDL_PEEKEVENT,
                                                (uint)event_mask );
            if ( ret == (int)NativeFunctionReturnFlags.Error )
                throw SdlException.Generate();

            SdlEventArgs[] eventargs_array = new SdlEventArgs[ret];
            for ( int i = 0; i < eventargs_array.Length; ++ i ) {
                if ( events[i].type == (byte)EventTypes.UserEvent ) {
                    eventargs_array[i] = __user_events[events[i].user.code.ToString()];
                    __user_events.Remove( events[i].user.code.ToString() );
                } else
                    eventargs_array[i] = SdlEventArgs.CreateEventArgs( events[i] );
            }
            return eventargs_array;
        }


        public static bool IsEventQueued(EventMask event_mask) {
            SdlEventArgs[] event_array = Peek( event_mask, QUERY_EVENTS_MAX );

            return event_array.Length > 0;
        }


        public static void IgnoreEvent(EventTypes event_type) {
            SdlSystem.SDL_EventState( (byte)event_type, SdlSystem.SDL_IGNORE );
        }


        public static void EnableEvent(EventTypes event_type) {
            SdlSystem.SDL_EventState( (byte)event_type, SdlSystem.SDL_ENABLE );
        }


        public static bool IsEventEnabled(EventTypes event_type) {
            return SdlSystem.SDL_EventState( (byte)event_type, SdlSystem.SDL_QUERY ) == SdlSystem.SDL_ENABLE;
        }


        public static void Close() {
            Active = null;

            KeyboardDown = null;
            KeyboardUp = null;

            MouseMotion = null;
            MouseButtonDown = null;
            MouseButtonUp = null;

            JoystickButtonDown = null;
            JoystickButtonUp = null;
            JoystickAxisMotion = null;
            JoystickHatMotion = null;
            JoystickBallMotion = null;

            VideoResize = null;
            VideoExpose = null;

            WindowManagerEvent = null;

            ChannelFinished = null;
            MusicFinished = null;

            Tick = null;

            Events.CloseJoysticks();
            Events.CloseCDRom();
            Events.CloseMixer();
            Events.CloseTimer();
            Events.CloseVideo();
            SdlSystem.SDL_Quit();
            Quit = null;
        }


        public static void CloseVideo() {
            try {
                CloseAnySystem( SdlSystem.SDL_INIT_VIDEO );
            } catch ( AccessViolationException ) {
            }
        }


        public static void CloseTimer() {
            try {
                CloseAnySystem( SdlSystem.SDL_INIT_TIMER );
            } catch ( AccessViolationException ) {
            }
        }


        public static void CloseJoysticks() {
            try {
                CloseAnySystem( SdlSystem.SDL_INIT_JOYSTICK );
            } catch ( AccessViolationException ) {
            }
        }


        public static void CloseCDRom() {
            try {
                CloseAnySystem( SdlSystem.SDL_INIT_CDROM );
            } catch ( AccessViolationException ) {
            }
        }

		
		/// <summary>
		/// Closes the mixer.
		/// </summary>
        public static void CloseMixer() {
            try {
                SdlMixerSystem.CloseMixer();
                CloseAnySystem( SdlSystem.SDL_INIT_AUDIO );
            } catch ( AccessViolationException ) {
            }
        }

		
		/// <summary>
		/// Closes the audio.
		/// </summary>
        public static void CloseAudio() {
            SdlSystem.SDL_CloseAudio();

            Mixer.AudioOpen = false;
            Mixer.AudioLocked = false;

            Events.CloseMixer();
        }

		
		/// <summary>
		/// Quits the application.
		/// </summary>
        public static void QuitApplication() {
            GC.Collect();
            __quit_flag = true;
        }

		
		/// <summary>
		/// Run this instance.
		/// </summary>
        public static void Run() {
            __last_tick = 0;
            __quit_flag = false;
            Timer.Initialize();
            ThreadTicker();
            Events.Close();
        }
#endregion


#region Private Methods
		/// <summary>
		/// Closes any system.
		/// </summary>
		/// <param name='system_flag'>
		/// System_flag.
		/// </param>
        private static void CloseAnySystem(int system_flag) {
            if ( SdlSystem.SDL_WasInit( (uint)system_flag ) != 0 )
                SdlSystem.SDL_QuitSubSystem( (uint)system_flag );
        }

		
		/// <summary>
		/// Processes the event.
		/// </summary>
		/// <param name='e'>
		/// E.
		/// </param>
        private static void ProcessEvent(SdlSystem.SDL_Event e) {
            switch ( (EventTypes)e.type ) {
                case EventTypes.ActiveEvent:
                    OnActiveEvent( ActiveEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.KeyDown:
                    OnKeyboardDown( KeyboardEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.KeyUp:
                    OnKeyboardUp( KeyboardEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.MouseMotion:
                    OnMouseMotion( MouseMotionEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.MouseButtonDown:
                    OnMouseButtonDown( MouseButtonEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.MouseButtonUp:
                    OnMouseButtonUp( MouseButtonEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.JoystickAxisMotion:
                    OnJoystickAxisMotion( JoystickAxisEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.JoystickBallMotion:
                    OnJoystickBallMotion( JoystickBallEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.JoystickHatMotion:
                    OnJoystickHatMotion( JoystickHatEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.JoystickButtonDown:
                    OnJoystickButtonDown( JoystickButtonEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.JoystickButtonUp:
                    OnJoystickButtonUp( JoystickButtonEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.VideoResize:
                    OnVideoResize( VideoResizeEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.VideoExpose:
                    OnVideoExpose( VideoExposeEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.Quit:
                    OnQuit( QuitEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.WindowManagerEvent:
                    OnWindowManagerEvent( WindowManagerEventArgs.CreateEventArgs( e ) );
                    break;

                case EventTypes.UserEvent:
                    OnUserEvent( UserEventArgs.CreateEventArgs( e ) );
                    break;
            }
        }

		
		/// <summary>
		/// Raises the active event event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnActiveEvent(ActiveEventArgs ergs) {
            if ( Active != null )
                Active( new Events(), ergs );
        }

		
		/// <summary>
		/// Raises the keyboard down event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnKeyboardDown(KeyboardEventArgs ergs) {
            if ( KeyboardDown != null )
                KeyboardDown( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the key up event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnKeyboardUp(KeyboardEventArgs ergs) {
            if ( KeyboardUp != null )
                KeyboardUp( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the mouse motion event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnMouseMotion(MouseMotionEventArgs ergs ) {
            if ( MouseMotion != null )
                MouseMotion( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the mouse button down event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnMouseButtonDown(MouseButtonEventArgs ergs ) {
            if ( MouseButtonDown != null )
                MouseButtonDown( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the mouse button up event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnMouseButtonUp(MouseButtonEventArgs ergs) {
            if ( MouseButtonUp != null )
                MouseButtonUp( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the joystick axis motion event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnJoystickAxisMotion(JoystickAxisEventArgs ergs) {
            if ( JoystickAxisMotion != null ) {
                if ( ergs.RawAxisValue < JoystickAxisEventArgs.JoystickThreshold * -1
                     || ergs.RawAxisValue > JoystickAxisEventArgs.JoystickThreshold )
                    JoystickAxisMotion( __instance, ergs );
            }
        }

		
		/// <summary>
		/// Raises the joystick ball motion event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnJoystickBallMotion(JoystickBallEventArgs ergs) {
            if ( JoystickBallMotion != null )
                JoystickBallMotion( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the joystick hat motion event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnJoystickHatMotion(JoystickHatEventArgs ergs) {
            if ( JoystickHatMotion != null )
                JoystickHatMotion( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the joystick button down event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnJoystickButtonDown(JoystickButtonEventArgs ergs) {
            if ( JoystickButtonDown != null )
                JoystickButtonDown( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the joystick button up event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnJoystickButtonUp(JoystickButtonEventArgs ergs) {
            if ( JoystickButtonUp != null )
                JoystickButtonUp( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the video resize event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnVideoResize(VideoResizeEventArgs ergs) {
            if ( VideoResize != null )
                VideoResize( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the video expose event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnVideoExpose(VideoExposeEventArgs ergs) {
            if ( VideoExpose != null )
                VideoExpose( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the quit event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnQuit(QuitEventArgs ergs) {
            if ( Quit != null )
                Quit( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the window manager event event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnWindowManagerEvent(WindowManagerEventArgs ergs) {
            if ( WindowManagerEvent != null )
                WindowManagerEvent( __instance, ergs );
        }

		
		/// <summary>
		/// Raises the user event event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        private static void OnUserEvent(UserEventArgs ergs) {
            if ( UserEvent != null || ChannelFinished != null || MusicFinished != null ) {
                UserEventArgs ret;

                lock( __instance ) {
                    ret = __user_events[ergs.UserCode.ToString()];
                }

                if ( ret != null ) {
                    if ( ret.GetType().Name == "ChannelFinishedEventArgs" ) {
                        if ( ChannelFinished != null )
                            ChannelFinished( __instance, (ChannelFinishedEventArgs)ret );
                    } else if ( ret.GetType().Name == "MusicFinishedEventArgs" ) {
                        if ( MusicFinished != null )
                            MusicFinished( __instance, (MusicFinishedEventArgs)ret );
                    } else {
                        if ( UserEvent != null )
                            UserEvent( __instance, ret );
                    }
                }
                __user_events.Remove( ergs.UserCode.ToString() );
            }
        }

		
		/// <summary>
		/// Threads the ticker.
		/// </summary>
        private static void ThreadTicker() {
            int frames = 0;
            int last_time = Timer.TickElapsed;
            int current_tick;
            int target_tick;

            while ( !__quit_flag ) {

                while ( Events.Poll() ) ;

                current_tick = Timer.TickElapsed;
                target_tick = last_time + (int)__ticks_per_frame;

                if ( current_tick <= target_tick )
                    Thread.Sleep( target_tick - current_tick );

                current_tick = Timer.TickElapsed;

                Events.OnTick( TickEventArgs.CreateEventArgs( current_tick, last_time, __fps ) );
                last_time = current_tick;

                ++ frames;

                if ( last_time + 1000 <= current_tick ) {
                    __fps = frames;
                    frames = 0;
                    last_time = current_tick;
                }
            }
        }
#endregion


#region Internal Methods
		/// <summary>
		/// Notifies the channel finished.
		/// </summary>
		/// <param name='channel'>
		/// Channel.
		/// </param>
        internal static void NotifyChannelFinished(int channel) {
            PushUserEvent( ChannelFinishedEventArgs.CreateEventArgs( channel ) );
        }

		
		/// <summary>
		/// Notifies the music finished.
		/// </summary>
        internal static void NotifyMusicFinished() {
            PushUserEvent( MusicFinishedEventArgs.CreateEventArgs() );
        }

		
		/// <summary>
		/// Raises the tick event.
		/// </summary>
		/// <param name='ergs'>
		/// Ergs.
		/// </param>
        internal static void OnTick(TickEventArgs ergs) {
            if ( Tick != null )
                Tick( __instance, ergs );
        }
#endregion


#region Private fields
        private static IDictionary<string, UserEventArgs> __user_events = new Dictionary<string, UserEventArgs>();
        private static int __user_event_id;
        private const int QUERY_EVENTS_MAX = 254;
        private static ResourceManager __string_manager;
        private static Events __instance = new Events();

        private static int __target_fps = 60;
        private static int __fps = 60;
        private static int __last_tick;
        private static float __ticks_per_frame = 1000.0f / (float)__target_fps;
        private static bool __quit_flag;
#endregion
    }


}
