// 
//  SdlEventArgs.cs
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


namespace Sdl.Core {

	
	using Sdl.Audio;
	using Sdl.Graphics;
	using Sdl.Input;

	
	/// <summary>
	/// Sdl event arguments.
	/// </summary>
    public class SdlEventArgs : EventArgs {
#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.SdlEventArgs"/> class.
		/// </summary>
        public SdlEventArgs() {
            this.event_struct_ = new SdlSystem.SDL_Event();
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.SdlEventArgs"/> class.
		/// </summary>
		/// <param name='event_struct'>
		/// Event_struct.
		/// </param>
        internal SdlEventArgs(SdlSystem.SDL_Event event_struct) {
            this.event_struct_ = event_struct;
        }
#endregion


#region Public Properties
		/// <summary>
		/// Gets or sets the event struct.
		/// </summary>
		/// <value>
		/// The event struct.
		/// </value>
        public SdlSystem.SDL_Event EventStruct {
            get { return this.event_struct_; }
            set { this.event_struct_ = value; }
        }

		
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>
		/// The type of the event.
		/// </value>
        public EventTypes EventType {
            get { return (EventTypes)this.event_struct_.type; }
			set { this.event_struct_.type = (byte)value; }
        }
#endregion


#region Protected Methods
		/// <summary>
		/// Creates the event arguments.
		/// </summary>
		/// <returns>
		/// The event arguments.
		/// </returns>
		/// <param name='ev'>
		/// The ${ParameterType} instance containing the event data.
		/// </param>
        internal static SdlEventArgs CreateEventArgs(SdlSystem.SDL_Event ev) {
            switch ( (EventTypes)ev.type ) {
                case EventTypes.ActiveEvent:
                    return new ActiveEventArgs( ev );

                case EventTypes.KeyDown:
                    return new KeyboardEventArgs( ev );

                case EventTypes.KeyUp:
                    return new KeyboardEventArgs( ev );

                case EventTypes.MouseMotion:
                    return new MouseMotionEventArgs( ev );

                case EventTypes.MouseButtonDown:
                    return new MouseButtonEventArgs( ev );

                case EventTypes.MouseButtonUp:
                    return new MouseButtonEventArgs( ev );

                case EventTypes.JoystickAxisMotion:
                    return new JoystickAxisEventArgs( ev );

                case EventTypes.JoystickBallMotion:
                    return new JoystickBallEventArgs( ev );

                case EventTypes.JoystickHatMotion:
                    return new JoystickHatEventArgs( ev );

                case EventTypes.JoystickButtonDown:
                    return new JoystickButtonEventArgs( ev );

                case EventTypes.JoystickButtonUp:
                    return new JoystickButtonEventArgs( ev );

                case EventTypes.VideoResize:
                    return new VideoResizeEventArgs( ev );

                case EventTypes.VideoExpose:
                    return new VideoExposeEventArgs( ev );

                case EventTypes.Quit:
                    return new QuitEventArgs( ev );

                case EventTypes.WindowManagerEvent:
                    return new WindowManagerEventArgs( ev );

                case EventTypes.UserEvent:
                    return new UserEventArgs( ev );

                default:
                    return new SdlEventArgs( ev );
            }
        }
#endregion


#region Private Fields
        protected internal SdlSystem.SDL_Event event_struct_;
#endregion
    }


}
