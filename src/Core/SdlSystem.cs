// 
//  SdlSystem.cs
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
using System.IO;
using System.Text;
using System.Runtime.InteropServices;


namespace Sdl.Core {

	
	/// <summary>
	/// Sdl system.
	/// </summary>
	public static class SdlSystem {
#region SDL_h
		public const int SDL_INIT_TIMER = 0x00000001;
		public const int SDL_INIT_AUDIO = 0x00000010;
		public const int SDL_INIT_VIDEO = 0x00000020;
		public const int SDL_INIT_CDROM = 0x00000100;
		public const int SDL_INIT_JOYSTICK = 0x00000200;
		public const int SDL_INIT_NOPARACHUTE = 0x00100000;  /**< Don't catch fatal signals */
		public const int SDL_INIT_EVENTTHREAD = 0x01000000;  /**< Not supported on all OS's */
		public const int SDL_INIT_EVERYTHING = 0x0000FFFF;

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='flags'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_Init(uint flags);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='flags'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_InitSubSystem(uint flags);

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name='flags'></param>
		[DllImport( "libSDL.so" )]
		public extern static void SDL_QuitSubSystem(uint flags);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='flags'></param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_WasInit(uint flags);

		
		/// <summary>
		/// 
		/// </summary>
		[DllImport( "libSDL.so" )]
		public extern static void SDL_Quit();
#endregion SDL_h


#region SDL_audio_h
        [Serializable]
        public struct SDL_AudioSpec {
            public int     freq;        /**< DSP frequency -- samples per second */
            public ushort  format;      /**< Audio data format */
            public byte    channels;    /**< Number of channels: 1 mono, 2 stereo */
            public byte    silence;     /**< Audio buffer silence value (calculated) */
            public ushort  samples;     /**< Audio buffer size in samples (power of 2) */
            public ushort  padding;     /**< Necessary for some compile environments */
            public uint    size;        /**< Audio buffer size in bytes (calculated) */
            /**
             *  This function is called when the audio device needs more data.
             *
             *  @param[out] stream  A pointer to the audio data buffer
             *  @param[in]  len The length of the audio buffer in bytes.
             *
             *  Once the callback returns, the buffer will no longer be valid.
             *  Stereo samples are stored in a LRLRLR ordering.
             */
            //void (SDLCALL *callback)(void *userdata, Uint8 *stream, int len);
            public SdlAudioSpecCallback callback;
            
            public IntPtr /*void  **/userdata;
        }
        public delegate void SdlAudioSpecCallback(IntPtr userdata, byte[] stream, int len);


        public const ushort AUDIO_U8 = 0x0008;  /**< Unsigned 8-bit samples */
        public const ushort AUDIO_S8 = 0x8008;  /**< Signed 8-bit samples */
        public const ushort AUDIO_U16LSB = 0x0010;  /**< Unsigned 16-bit samples */
        public const ushort AUDIO_S16LSB = 0x8010;  /**< Signed 16-bit samples */
        public const ushort AUDIO_U16MSB = 0x1010;  /**< As above, but big-endian byte order */
        public const ushort AUDIO_S16MSB = 0x9010;  /**< As above, but big-endian byte order */
        public const ushort AUDIO_U16 = AUDIO_U16LSB;
        public const ushort AUDIO_S16 = AUDIO_S16LSB;


        /** A structure to hold a set of audio conversion filters and buffers */
        [Serializable]
        public struct SDL_AudioCVT {
            public int needed;         /**< Set to 1 if conversion possible */
            public ushort src_format;      /**< Source audio format */
            public ushort dst_format;      /**< Target audio format */
            public double rate_incr;       /**< Rate conversion increment */
            public byte[] buf;         /**< Buffer to hold entire audio data */
            public int    len;         /**< Length of original audio buffer */
            public int    len_cvt;         /**< Length of converted audio buffer */
            public int    len_mult;        /**< buffer must be len*len_mult big */
            public double len_ratio;   /**< Given len, final size is len*len_ratio */
            //void (SDLCALL *filters[10])(struct SDL_AudioCVT *cvt, Uint16 format);
            public SdlAudioCvtFilter[] filters;
            public int filter_index;       /**< Current audio conversion function */
        }

        public delegate void SdlAudioCvtFilter(SDL_AudioCVT cvt, ushort format);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_AudioInit(string driver_name);


        [DllImport( "libSDL.so" )]
		public extern static void SDL_AudioQuit();


        [DllImport( "libSDL.so" )]
		public extern static StringBuilder SDL_AudioDriverName(StringBuilder namebuf, int maxlen);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_OpenAudio(SDL_AudioSpec desired, out SDL_AudioSpec obtained);


        public enum SDL_audiostatus {
            SDL_AUDIO_STOPPED = 0,
            SDL_AUDIO_PLAYING,
            SDL_AUDIO_PAUSED
        }


        [DllImport( "libSDL.so" )]
		public extern static SDL_audiostatus SDL_GetAudioStatus();


        [DllImport( "libSDL.so" )]
		public extern static void SDL_PauseAudio(int pause_on);


        [DllImport( "libSDL.so" )]
		public extern static SDL_AudioSpec SDL_LoadWAV_RW( SDL_RWops src,
                                                           int freesrc,
                                                           SDL_AudioSpec spec,
                                                           byte[] audio_buf,
                                                           out uint audio_len);

		
		/// <summary>
		/// SDs the l_ load WA.
		/// </summary>
		/// <returns>
		/// The l_ load WA.
		/// </returns>
		/// <param name='file'>
		/// File.
		/// </param>
		/// <param name='spec'>
		/// Spec.
		/// </param>
		/// <param name='audio_buf'>
		/// Audio_buf.
		/// </param>
		/// <param name='audio_len'>
		/// Audio_len.
		/// </param>
        public static SDL_AudioSpec SDL_LoadWAV(FileInfo file, SDL_AudioSpec spec, byte[] audio_buf, out uint audio_len) {
            return SDL_LoadWAV_RW( SDL_RWFromFile( file.FullName, "rb" ), 1, spec, audio_buf, out audio_len );
        }
		/// <summary>
		/// SDs the l_ load WA.
		/// </summary>
		/// <returns>
		/// The l_ load WA.
		/// </returns>
		/// <param name='file'>
		/// File.
		/// </param>
		/// <param name='spec'>
		/// Spec.
		/// </param>
		/// <param name='audio_buf'>
		/// Audio_buf.
		/// </param>
		/// <param name='audio_len'>
		/// Audio_len.
		/// </param>
		public static SDL_AudioSpec SDL_LoadWAV(string file, SDL_AudioSpec spec, byte[] audio_buf, out uint audio_len) {
            return SDL_LoadWAV_RW( SDL_RWFromFile( file, "rb" ), 1, spec, audio_buf, out audio_len );
        }


        [DllImport( "libSDL.so" )]
		public extern static void SDL_FreeWAV(byte[] audio_buf);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_BuildAudioCVT( out SDL_AudioCVT cvt,
                                                    ushort src_format, byte src_channels, int src_rate,
                                                    ushort dst_format, byte dst_channels, int dst_rate );


        [DllImport( "libSDL.so" )]
		public extern static int SDL_ConvertAudio(out SDL_AudioCVT cvt);


        public const int SDL_MIX_MAXVOLUME = 128;


        [DllImport( "libSDL.so" )]
		public extern static void SDL_MixAudio(out byte[] dst, byte[] src, uint len, int volume);


        [DllImport( "libSDL.so" )]
		public extern static void SDL_LockAudio();


        [DllImport( "libSDL.so" )]
		public extern static void SDL_UnlockAudio();


        [DllImport( "libSDL.so" )]
		public extern static void SDL_CloseAudio();
#endregion SDL_audio_h


#region SDL_cdrom_h
        public const int SDL_MAX_TRACKS = 99;
        public const int SDL_AUDIO_TRACK = 0x00;
        public const int SDL_DATA_TRACK = 0x04;


        public enum CDstatus {
            CD_TRAYEMPTY,
            CD_STOPPED,
            CD_PLAYING,
            CD_PAUSED,
            CD_ERROR = -1
        }

		
		[Serializable]
        public struct SDL_CDtrack {
            public byte id;
            public byte type;
            public ushort unused;
            public uint length;
            public uint offset;
        }

		
		[Serializable]
        public struct SDL_CD {
            public int id;
            public CDstatus status;

            public int numtracks;
            public int cur_track;
            public int cur_frame;
            public SDL_CD[] track;
        }


        public const int CD_FPS = 75;


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDNumDirives();


        [DllImport( "libSDL.so" )]
		public extern static string SDL_CDName(int drive);


        [DllImport( "libSDL.so" )]
		public extern static SDL_CD SDL_CDOpen(int drive);


        [DllImport( "libSDL.so" )]
		public extern static CDstatus SDL_CDStatus(SDL_CD cdrom);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDPlayTracks(SDL_CD cdrom, int start_track, int start_frame, int ntracks, int nframes);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDPlay(SDL_CD cdrom, int start, int length);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDPause(SDL_CD cdrom);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDResume(SDL_CD cdrom);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDStop(SDL_CD cdrom);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDEject(SDL_CD cdrom);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_CDClose(SDL_CD cdrom);
#endregion SDL_cdrom_h


#region SDL_error_h
        [DllImport( "libSDL.so" )]
		public extern static void  SDL_SetError(string format, params object[] arguments);


        [DllImport( "libSDL.so" )]
		public extern static string SDL_GetError();


        [DllImport( "libSDL.so" )]
		public extern static void SDL_ClearError();
#endregion SDL_error_h


#region SDL_events_h
		/// <summary>
		/// 
		/// </summary>
		public enum SDL_EventType {
			SDL_NOEVENT = 0,         /**< Unused (do not remove) */
			SDL_ACTIVEEVENT,         /**< Application loses/gains visibility */
			SDL_KEYDOWN,         /**< Keys pressed */
			SDL_KEYUP,           /**< Keys released */
			SDL_MOUSEMOTION,         /**< Mouse moved */
			SDL_MOUSEBUTTONDOWN,     /**< Mouse button pressed */
			SDL_MOUSEBUTTONUP,       /**< Mouse button released */
			SDL_JOYAXISMOTION,       /**< Joystick axis motion */
			SDL_JOYBALLMOTION,       /**< Joystick trackball motion */
			SDL_JOYHATMOTION,        /**< Joystick hat position change */
			SDL_JOYBUTTONDOWN,       /**< Joystick button pressed */
			SDL_JOYBUTTONUP,         /**< Joystick button released */
			SDL_QUIT,            /**< User-requested quit */
			SDL_SYSWMEVENT,          /**< System specific event */
			SDL_EVENT_RESERVEDA,     /**< Reserved for future use.. */
			SDL_EVENT_RESERVEDB,     /**< Reserved for future use.. */
			SDL_VIDEORESIZE,         /**< User resized video mode */
			SDL_VIDEOEXPOSE,         /**< Screen needs to be redrawn */
			SDL_EVENT_RESERVED2,     /**< Reserved for future use.. */
			SDL_EVENT_RESERVED3,     /**< Reserved for future use.. */
			SDL_EVENT_RESERVED4,     /**< Reserved for future use.. */
			SDL_EVENT_RESERVED5,     /**< Reserved for future use.. */
			SDL_EVENT_RESERVED6,     /**< Reserved for future use.. */
			SDL_EVENT_RESERVED7,     /**< Reserved for future use.. */
			/** Events SDL_USEREVENT through SDL_MAXEVENTS-1 are for your use */
			SDL_USEREVENT = 24,
			/** This last event is only for bounding internal arrays
             *  It is the number of bits in the event mask datatype -- Uint32
             */
			SDL_NUMEVENTS = 32
		}
        
		
		/// <summary>
		/// 
		/// </summary>
		public enum SDL_EventMask {
			SDL_ACTIVEEVENTMASK = 1 << (int)SDL_EventType.SDL_ACTIVEEVENT,
			SDL_KEYDOWNMASK     = 1 << (int)SDL_EventType.SDL_KEYDOWN,
			SDL_KEYUPMASK       = 1 << (int)SDL_EventType.SDL_KEYUP,
			SDL_KEYEVENTMASK    = 1 << (int)SDL_EventType.SDL_KEYDOWN | 1 << (int)SDL_EventType.SDL_KEYUP,
			SDL_MOUSEMOTIONMASK = 1 << (int)SDL_EventType.SDL_MOUSEMOTION,
			SDL_MOUSEBUTTONDOWNMASK = 1 << (int)SDL_EventType.SDL_MOUSEBUTTONDOWN,
			SDL_MOUSEBUTTONUPMASK   = 1 << (int)SDL_EventType.SDL_MOUSEBUTTONUP,
			SDL_MOUSEEVENTMASK  = 1 << (int)SDL_EventType.SDL_MOUSEMOTION | 1 << (int)SDL_EventType.SDL_MOUSEBUTTONDOWN | 1 << (int)SDL_EventType.SDL_MOUSEBUTTONUP,
			SDL_JOYAXISMOTIONMASK   = 1 << (int)SDL_EventType.SDL_JOYAXISMOTION,
			SDL_JOYBALLMOTIONMASK   = 1 << (int)SDL_EventType.SDL_JOYBALLMOTION,
			SDL_JOYHATMOTIONMASK    = 1 << (int)SDL_EventType.SDL_JOYHATMOTION,
			SDL_JOYBUTTONDOWNMASK   = 1 << (int)SDL_EventType.SDL_JOYBUTTONDOWN,
			SDL_JOYBUTTONUPMASK = 1 << (int)SDL_EventType.SDL_JOYBUTTONUP,
			SDL_JOYEVENTMASK    = 1 << (int)SDL_EventType.SDL_JOYAXISMOTION | 1 << (int)SDL_EventType.SDL_JOYBALLMOTION
            | 1 << (int)SDL_EventType.SDL_JOYHATMOTION | 1 << (int)SDL_EventType.SDL_JOYBUTTONDOWN | 1 << (int)SDL_EventType.SDL_JOYBUTTONUP,
			SDL_VIDEORESIZEMASK = 1 << (int)SDL_EventType.SDL_VIDEORESIZE,
			SDL_VIDEOEXPOSEMASK = 1 << (int)SDL_EventType.SDL_VIDEOEXPOSE,
			SDL_QUITMASK        = 1 << (int)SDL_EventType.SDL_QUIT,
			SDL_SYSWMEVENTMASK  = 1 << (int)SDL_EventType.SDL_SYSWMEVENT
		}

		
		/// <summary>
		/// 
		/// </summary>
		public const uint SDL_ALLEVENTS = 0xFFFFFFFF;


		/** Application visibility event structure */
		[Serializable]
        public struct SDL_ActiveEvent {
			public byte type; /**< SDL_ACTIVEEVENT */
			public byte gain; /**< Whether given states were gained or lost (1/0) */
			public byte state;    /**< A mask of the focus states */
		};

		/** Keyboard event structure */
		[Serializable]
        public struct SDL_KeyboardEvent {
			public byte type; /**< SDL_KEYDOWN or SDL_KEYUP */
			public byte which;    /**< The keyboard device index */
			public byte state;    /**< SDL_PRESSED or SDL_RELEASED */
			public SDL_keysym keysym;
		}

/** Mouse motion event structure */
		[Serializable]
        public struct SDL_MouseMotionEvent {
			public byte type; /**< SDL_MOUSEMOTION */
			public byte which;    /**< The mouse device index */
			public byte state;    /**< The current button state */
			public ushort x, y;    /**< The X/Y coordinates of the mouse */
			public short xrel;    /**< The relative motion in the X direction */
			public short yrel;    /**< The relative motion in the Y direction */
		}

/** Mouse button event structure */
		[Serializable]
        public struct SDL_MouseButtonEvent {
			public byte type; /**< SDL_MOUSEBUTTONDOWN or SDL_MOUSEBUTTONUP */
			public byte which;    /**< The mouse device index */
			public byte button;   /**< The mouse button index */
			public byte state;    /**< SDL_PRESSED or SDL_RELEASED */
			public ushort x, y;    /**< The X/Y coordinates of the mouse at press time */
		};

/** Joystick axis motion event structure */
		[Serializable]
        public struct SDL_JoyAxisEvent {
			public byte type; /**< SDL_JOYAXISMOTION */
			public byte which;    /**< The joystick device index */
			public byte axis; /**< The joystick axis index */
			public short value;   /**< The axis value (range: -32768 to 32767) */
		}

/** Joystick trackball motion event structure */
		[Serializable]
        public struct SDL_JoyBallEvent {
			public byte type; /**< SDL_JOYBALLMOTION */
			public byte which;    /**< The joystick device index */
			public byte ball; /**< The joystick trackball index */
			public short xrel;    /**< The relative motion in the X direction */
			public short yrel;    /**< The relative motion in the Y direction */
		}

/** Joystick hat position change event structure */
		[Serializable]
        public struct SDL_JoyHatEvent {
			public byte type; /**< SDL_JOYHATMOTION */
			public byte which;    /**< The joystick device index */
			public byte hat;  /**< The joystick hat index */
			public byte value;    /**< The hat position value:
                             *   SDL_HAT_LEFTUP   SDL_HAT_UP       SDL_HAT_RIGHTUP
                             *   SDL_HAT_LEFT     SDL_HAT_CENTERED SDL_HAT_RIGHT
                             *   SDL_HAT_LEFTDOWN SDL_HAT_DOWN     SDL_HAT_RIGHTDOWN
                             *  Note that zero means the POV is centered.
                             */
		}

/** Joystick button event structure */
		[Serializable]
        public struct SDL_JoyButtonEvent {
			public byte type; /**< SDL_JOYBUTTONDOWN or SDL_JOYBUTTONUP */
			public byte which;    /**< The joystick device index */
			public byte button;   /**< The joystick button index */
			public byte state;    /**< SDL_PRESSED or SDL_RELEASED */
		}

/** The "window resized" event
 *  When you get this event, you are responsible for setting a new video
 *  mode with the new width and height.
 */
		[Serializable]
        public struct SDL_ResizeEvent {
			public byte type; /**< SDL_VIDEORESIZE */
			public int w;      /**< New width */
			public int h;      /**< New height */
		}

/** The "screen redraw" event */
		[Serializable]
        public struct SDL_ExposeEvent {
			public byte type; /**< SDL_VIDEOEXPOSE */
		}

/** The "quit requested" event */
		[Serializable]
        public struct SDL_QuitEvent {
			public byte type; /**< SDL_QUIT */
		}

		
		/** A user-defined event type */
		[Serializable]
        public struct SDL_UserEvent {
			public byte type; /**< SDL_USEREVENT through SDL_NUMEVENTS-1 */
			public int code;   /**< User defined event code */
			public IntPtr/*void **/ data1;    /**< User defined data pointer */
			public IntPtr/*void **/ data2;    /**< User defined data pointer */
		}

		
		/** If you want to use this event, you should include SDL_syswm.h */
		// struct SDL_SysWMmsg;
		// public struct SDL_SysWMmsg SDL_SysWMmsg;
		[Serializable]
        public struct SDL_SysWMEvent {
			public byte type;
			public IntPtr/*SDL_SysWMmsg **/ msg;
		};

		/** General event structure */
		[StructLayout(LayoutKind.Explicit)]
        public /*union*/struct SDL_Event {
			[FieldOffset(0)]
			public byte type;
			[FieldOffset(0)]
			public SDL_ActiveEvent active;
			[FieldOffset(0)]
			public SDL_KeyboardEvent key;
			[FieldOffset(0)]
			public SDL_MouseMotionEvent motion;
			[FieldOffset(0)]
			public SDL_MouseButtonEvent button;
			[FieldOffset(0)]
			public SDL_JoyAxisEvent jaxis;
			[FieldOffset(0)]
			public SDL_JoyBallEvent jball;
			[FieldOffset(0)]
			public SDL_JoyHatEvent jhat;
			[FieldOffset(0)]
			public SDL_JoyButtonEvent jbutton;
			[FieldOffset(0)]
			public SDL_ResizeEvent resize;
			[FieldOffset(0)]
			public SDL_ExposeEvent expose;
			[FieldOffset(0)]
			public SDL_QuitEvent quit;
			[FieldOffset(0)]
			public SDL_UserEvent user;
			[FieldOffset(0)]
			public SDL_SysWMEvent syswm;
		};

		
		/// <summary>
		/// 
		/// </summary>
		[DllImport( "libSDL.so" )]
		public extern static void SDL_PumpEvents();

		
		/// <summary>
		/// 
		/// </summary>
		public enum SDL_eventaction {
			SDL_ADDEVENT,
			SDL_PEEKEVENT,
			SDL_GETEVENT
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='events'>
		/// 
		/// </param>
		/// <param name='numevents'>
		/// 
		/// </param>
		/// <param name='action'>
		/// 
		/// </param>
		/// <param name='mask'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_PeepEvents(out SDL_Event[] events,
                                                 int numevents,
                                                 SDL_eventaction action,
                                                 uint mask);
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='_event'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_PeepEvent(out SDL_Event _event);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='_event'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_PollEvent(out SDL_Event _event);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='_event'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_WaitEvent(out SDL_Event _event);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='_event'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static int SDL_PushEvent(out SDL_Event _event);

		
		/// <summary>
		/// 
		/// </summary>
		public delegate int SDL_EventFilter(SDL_Event _event);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='filter'>
		/// 
		/// </param>
		[DllImport( "libSDL.so" )]
		public extern static SDL_EventFilter SDL_SetEventFilter(SDL_EventFilter filter);

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[DllImport( "libSDL.so" )]
		public extern static SDL_EventFilter SDL_GetEventFilter();


		public const int SDL_QUERY = -1;
		public const int SDL_IGNORE = 0;
		public const int SDL_DISABLE = 0;
		public const int SDL_ENABLE = 1;

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		/// <param name='type'></param>
		/// <param name='state'></param>
		[DllImport( "libSDL.so" )]
		public extern static byte SDL_EventState(byte type, int state);
            

#endregion SDL_events_h


#region SDL_keyboard_h
		/// <summary>
		/// SD l_keysym.
		/// </summary>
		public struct SDL_keysym {
			public byte scancode;
			public SDLKey sym;
			public SDLMod mod;
			public ushort unicode;
		}
#endregion SDL_keyboard_h


#region SDL_keysym_h
		/// <summary>
		/// SDL key.
		/// </summary>
		public enum SDLKey {
			/** @name ASCII mapped keysyms
             *  The keyboard syms have been cleverly chosen to map to ASCII
             */
			/*@{*/
			SDLK_UNKNOWN        = 0,
			SDLK_FIRST      = 0,
			SDLK_BACKSPACE      = 8,
			SDLK_TAB        = 9,
			SDLK_CLEAR      = 12,
			SDLK_RETURN     = 13,
			SDLK_PAUSE      = 19,
			SDLK_ESCAPE     = 27,
			SDLK_SPACE      = 32,
			SDLK_EXCLAIM        = 33,
			SDLK_QUOTEDBL       = 34,
			SDLK_HASH       = 35,
			SDLK_DOLLAR     = 36,
			SDLK_AMPERSAND      = 38,
			SDLK_QUOTE      = 39,
			SDLK_LEFTPAREN      = 40,
			SDLK_RIGHTPAREN     = 41,
			SDLK_ASTERISK       = 42,
			SDLK_PLUS       = 43,
			SDLK_COMMA      = 44,
			SDLK_MINUS      = 45,
			SDLK_PERIOD     = 46,
			SDLK_SLASH      = 47,
			SDLK_0          = 48,
			SDLK_1          = 49,
			SDLK_2          = 50,
			SDLK_3          = 51,
			SDLK_4          = 52,
			SDLK_5          = 53,
			SDLK_6          = 54,
			SDLK_7          = 55,
			SDLK_8          = 56,
			SDLK_9          = 57,
			SDLK_COLON      = 58,
			SDLK_SEMICOLON      = 59,
			SDLK_LESS       = 60,
			SDLK_EQUALS     = 61,
			SDLK_GREATER        = 62,
			SDLK_QUESTION       = 63,
			SDLK_AT         = 64,
			/* 
               Skip uppercase letters
            */
			SDLK_LEFTBRACKET    = 91,
			SDLK_BACKSLASH      = 92,
			SDLK_RIGHTBRACKET   = 93,
			SDLK_CARET      = 94,
			SDLK_UNDERSCORE     = 95,
			SDLK_BACKQUOTE      = 96,
			SDLK_a          = 97,
			SDLK_b          = 98,
			SDLK_c          = 99,
			SDLK_d          = 100,
			SDLK_e          = 101,
			SDLK_f          = 102,
			SDLK_g          = 103,
			SDLK_h          = 104,
			SDLK_i          = 105,
			SDLK_j          = 106,
			SDLK_k          = 107,
			SDLK_l          = 108,
			SDLK_m          = 109,
			SDLK_n          = 110,
			SDLK_o          = 111,
			SDLK_p          = 112,
			SDLK_q          = 113,
			SDLK_r          = 114,
			SDLK_s          = 115,
			SDLK_t          = 116,
			SDLK_u          = 117,
			SDLK_v          = 118,
			SDLK_w          = 119,
			SDLK_x          = 120,
			SDLK_y          = 121,
			SDLK_z          = 122,
			SDLK_DELETE     = 127,
			/* End of ASCII mapped keysyms */
			/*@}*/

			/** @name International keyboard syms */
			/*@{*/
			SDLK_WORLD_0        = 160,      /* 0xA0 */
			SDLK_WORLD_1        = 161,
			SDLK_WORLD_2        = 162,
			SDLK_WORLD_3        = 163,
			SDLK_WORLD_4        = 164,
			SDLK_WORLD_5        = 165,
			SDLK_WORLD_6        = 166,
			SDLK_WORLD_7        = 167,
			SDLK_WORLD_8        = 168,
			SDLK_WORLD_9        = 169,
			SDLK_WORLD_10       = 170,
			SDLK_WORLD_11       = 171,
			SDLK_WORLD_12       = 172,
			SDLK_WORLD_13       = 173,
			SDLK_WORLD_14       = 174,
			SDLK_WORLD_15       = 175,
			SDLK_WORLD_16       = 176,
			SDLK_WORLD_17       = 177,
			SDLK_WORLD_18       = 178,
			SDLK_WORLD_19       = 179,
			SDLK_WORLD_20       = 180,
			SDLK_WORLD_21       = 181,
			SDLK_WORLD_22       = 182,
			SDLK_WORLD_23       = 183,
			SDLK_WORLD_24       = 184,
			SDLK_WORLD_25       = 185,
			SDLK_WORLD_26       = 186,
			SDLK_WORLD_27       = 187,
			SDLK_WORLD_28       = 188,
			SDLK_WORLD_29       = 189,
			SDLK_WORLD_30       = 190,
			SDLK_WORLD_31       = 191,
			SDLK_WORLD_32       = 192,
			SDLK_WORLD_33       = 193,
			SDLK_WORLD_34       = 194,
			SDLK_WORLD_35       = 195,
			SDLK_WORLD_36       = 196,
			SDLK_WORLD_37       = 197,
			SDLK_WORLD_38       = 198,
			SDLK_WORLD_39       = 199,
			SDLK_WORLD_40       = 200,
			SDLK_WORLD_41       = 201,
			SDLK_WORLD_42       = 202,
			SDLK_WORLD_43       = 203,
			SDLK_WORLD_44       = 204,
			SDLK_WORLD_45       = 205,
			SDLK_WORLD_46       = 206,
			SDLK_WORLD_47       = 207,
			SDLK_WORLD_48       = 208,
			SDLK_WORLD_49       = 209,
			SDLK_WORLD_50       = 210,
			SDLK_WORLD_51       = 211,
			SDLK_WORLD_52       = 212,
			SDLK_WORLD_53       = 213,
			SDLK_WORLD_54       = 214,
			SDLK_WORLD_55       = 215,
			SDLK_WORLD_56       = 216,
			SDLK_WORLD_57       = 217,
			SDLK_WORLD_58       = 218,
			SDLK_WORLD_59       = 219,
			SDLK_WORLD_60       = 220,
			SDLK_WORLD_61       = 221,
			SDLK_WORLD_62       = 222,
			SDLK_WORLD_63       = 223,
			SDLK_WORLD_64       = 224,
			SDLK_WORLD_65       = 225,
			SDLK_WORLD_66       = 226,
			SDLK_WORLD_67       = 227,
			SDLK_WORLD_68       = 228,
			SDLK_WORLD_69       = 229,
			SDLK_WORLD_70       = 230,
			SDLK_WORLD_71       = 231,
			SDLK_WORLD_72       = 232,
			SDLK_WORLD_73       = 233,
			SDLK_WORLD_74       = 234,
			SDLK_WORLD_75       = 235,
			SDLK_WORLD_76       = 236,
			SDLK_WORLD_77       = 237,
			SDLK_WORLD_78       = 238,
			SDLK_WORLD_79       = 239,
			SDLK_WORLD_80       = 240,
			SDLK_WORLD_81       = 241,
			SDLK_WORLD_82       = 242,
			SDLK_WORLD_83       = 243,
			SDLK_WORLD_84       = 244,
			SDLK_WORLD_85       = 245,
			SDLK_WORLD_86       = 246,
			SDLK_WORLD_87       = 247,
			SDLK_WORLD_88       = 248,
			SDLK_WORLD_89       = 249,
			SDLK_WORLD_90       = 250,
			SDLK_WORLD_91       = 251,
			SDLK_WORLD_92       = 252,
			SDLK_WORLD_93       = 253,
			SDLK_WORLD_94       = 254,
			SDLK_WORLD_95       = 255,      /* 0xFF */
			/*@}*/

			/** @name Numeric keypad */
			/*@{*/
			SDLK_KP0        = 256,
			SDLK_KP1        = 257,
			SDLK_KP2        = 258,
			SDLK_KP3        = 259,
			SDLK_KP4        = 260,
			SDLK_KP5        = 261,
			SDLK_KP6        = 262,
			SDLK_KP7        = 263,
			SDLK_KP8        = 264,
			SDLK_KP9        = 265,
			SDLK_KP_PERIOD      = 266,
			SDLK_KP_DIVIDE      = 267,
			SDLK_KP_MULTIPLY    = 268,
			SDLK_KP_MINUS       = 269,
			SDLK_KP_PLUS        = 270,
			SDLK_KP_ENTER       = 271,
			SDLK_KP_EQUALS      = 272,
			/*@}*/

			/** @name Arrows + Home/End pad */
			/*@{*/
			SDLK_UP         = 273,
			SDLK_DOWN       = 274,
			SDLK_RIGHT      = 275,
			SDLK_LEFT       = 276,
			SDLK_INSERT     = 277,
			SDLK_HOME       = 278,
			SDLK_END        = 279,
			SDLK_PAGEUP     = 280,
			SDLK_PAGEDOWN       = 281,
			/*@}*/

			/** @name Function keys */
			/*@{*/
			SDLK_F1         = 282,
			SDLK_F2         = 283,
			SDLK_F3         = 284,
			SDLK_F4         = 285,
			SDLK_F5         = 286,
			SDLK_F6         = 287,
			SDLK_F7         = 288,
			SDLK_F8         = 289,
			SDLK_F9         = 290,
			SDLK_F10        = 291,
			SDLK_F11        = 292,
			SDLK_F12        = 293,
			SDLK_F13        = 294,
			SDLK_F14        = 295,
			SDLK_F15        = 296,
			/*@}*/

			/** @name Key state modifier keys */
			/*@{*/
			SDLK_NUMLOCK        = 300,
			SDLK_CAPSLOCK       = 301,
			SDLK_SCROLLOCK      = 302,
			SDLK_RSHIFT     = 303,
			SDLK_LSHIFT     = 304,
			SDLK_RCTRL      = 305,
			SDLK_LCTRL      = 306,
			SDLK_RALT       = 307,
			SDLK_LALT       = 308,
			SDLK_RMETA      = 309,
			SDLK_LMETA      = 310,
			SDLK_LSUPER     = 311,      /**< Left "Windows" key */
			SDLK_RSUPER     = 312,      /**< Right "Windows" key */
			SDLK_MODE       = 313,      /**< "Alt Gr" key */
			SDLK_COMPOSE        = 314,      /**< Multi-key compose key */
			/*@}*/

			/** @name Miscellaneous function keys */
			/*@{*/
			SDLK_HELP       = 315,
			SDLK_PRINT      = 316,
			SDLK_SYSREQ     = 317,
			SDLK_BREAK      = 318,
			SDLK_MENU       = 319,
			SDLK_POWER      = 320,      /**< Power Macintosh power key */
			SDLK_EURO       = 321,      /**< Some european keyboards */
			SDLK_UNDO       = 322,      /**< Atari keyboard has Undo */
			/*@}*/

			/* Add any other keys here */

			SDLK_LAST
		}


		public enum SDLMod {
			KMOD_NONE  = 0x0000,
			KMOD_LSHIFT= 0x0001,
			KMOD_RSHIFT= 0x0002,
			KMOD_LCTRL = 0x0040,
			KMOD_RCTRL = 0x0080,
			KMOD_LALT  = 0x0100,
			KMOD_RALT  = 0x0200,
			KMOD_LMETA = 0x0400,
			KMOD_RMETA = 0x0800,
			KMOD_NUM   = 0x1000,
			KMOD_CAPS  = 0x2000,
			KMOD_MODE  = 0x4000,
			KMOD_RESERVED = 0x8000
		}
#endregion SDL_keysym_h


#region SDL_timer_h
        public const int SDL_TIMESLICE = 10;


        public const int TIMER_RESOLUTION = 10;

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        [DllImport( "libSDL.so" )]
		public extern static uint SDL_GetTicks();


        [DllImport( "libSDL.so" )]
		public extern static void SDL_Delay(uint ms);


        public delegate uint SDL_TimerCallback(uint interval);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_SetTimer(uint interval, SDL_TimerCallback callback);


        public delegate uint SDL_NewTimerCallback(uint interval, IntPtr param);


        [DllImport( "libSDL.so" )]
		public extern static IntPtr SDL_AddTimer(uint interval, SDL_NewTimerCallback callback, IntPtr param);


        [DllImport( "libSDL.so" )]
		public extern static bool SDL_RemoveTimer(IntPtr t);
#endregion SDL_timer_h


#region SDL_rwops_h
        public struct SDL_RWops {
            public SdlRwopsSeekCallback seek;
            public SdlRwopsReadCallback read;
            public SdlRwopsWriteCallback write;
            public SdlRwopsCloseCallback close;
            public uint type;

            [StructLayout(LayoutKind.Explicit)]
            public struct __x001 {
                public struct __x0011 {
                    public IntPtr _base;
                    public IntPtr here;
                    public IntPtr stop;
                }
                [FieldOffset(0)]
                public __x0011 mem;
                
                public struct __x0012 {
                    public IntPtr data1;
                }
                [FieldOffset(0)]
                public __x0012 unknown;
            }
            public __x001 hidden;
        }

        public delegate int SdlRwopsSeekCallback(SDL_RWops context, int offset, int whence);
        public delegate int SdlRwopsReadCallback(SDL_RWops context, out IntPtr ptr, int size, int maxnum);
        public delegate int SdlRwopsWriteCallback(SDL_RWops context, IntPtr ptr, int size, int num);
        public delegate int SdlRwopsCloseCallback(SDL_RWops context);


        [DllImport( "libSDL.so" )]
		public extern static SDL_RWops SDL_RWFromFile(string file, string mode);


        [DllImport( "libSDL.so" )]
		public extern static SDL_RWops SDL_RWFromMem(MemoryStream mem, int size);


        [DllImport( "libSDL.so" )]
		public extern static SDL_RWops SDL_RWFromConstMem(byte[] mem, int size);


        [DllImport( "libSDL.so" )]
		public extern static SDL_RWops SDL_AllocRW();


        [DllImport( "libSDL.so" )]
		public extern static void SDL_FreeRW(SDL_RWops area);


        public const int RW_SEEK_SET = 0;   /**< Seek from the beginning of data */
        public const int RW_SEEK_CUR = 1;   /**< Seek relative to current read point */
        public const int RW_SEEK_END = 2;   /**< Seek relative to the end of data */


        [DllImport( "libSDL.so" )]
		public extern static ushort SDL_ReadLE16(SDL_RWops src);


        [DllImport( "libSDL.so" )]
		public extern static ushort SDL_ReadBE16(SDL_RWops src);


        [DllImport( "libSDL.so" )]
		public extern static uint SDL_ReadLE32(SDL_RWops src);


        [DllImport( "libSDL.so" )]
		public extern static uint SDL_ReadBE32(SDL_RWops src);


        [DllImport( "libSDL.so" )]
		public extern static ulong SDL_ReadLE64(SDL_RWops src);


        [DllImport( "libSDL.so" )]
		public extern static ulong SDL_ReadBE64(SDL_RWops src);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_WriteLE16(SDL_RWops src, ushort value);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_WriteBE16(SDL_RWops src, ushort value);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_WriteLE32(SDL_RWops src, uint value);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_WriteBE32(SDL_RWops src, uint value);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_WriteLE64(SDL_RWops src, ulong value);


        [DllImport( "libSDL.so" )]
		public extern static int SDL_WriteBE64(SDL_RWops src, ulong value);
        
#endregion SDL_rwops_h        
	}


}
