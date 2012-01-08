// 
//  EventTypes.cs
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

	
	/// <summary>
	/// Event types.
	/// </summary>
    [FlagsAttribute]
    public enum EventTypes {
		/// <summary>
		/// Constant none.
		/// </summary>
        None = SdlSystem.SDL_EventType.SDL_NOEVENT,
		/// <summary>
		/// Constant active event.
		/// </summary>
        ActiveEvent = SdlSystem.SDL_EventType.SDL_ACTIVEEVENT,
		/// <summary>
		/// Constant key down.
		/// </summary>
        KeyDown = SdlSystem.SDL_EventType.SDL_KEYDOWN,
		/// <summary>
		/// Constant key up.
		/// </summary>
        KeyUp = SdlSystem.SDL_EventType.SDL_KEYUP,
		/// <summary>
		/// Constant mouse motion.
		/// </summary>
        MouseMotion = SdlSystem.SDL_EventType.SDL_MOUSEMOTION,
		/// <summary>
		/// Constant mouse button down.
		/// </summary>
        MouseButtonDown = SdlSystem.SDL_EventType.SDL_MOUSEBUTTONDOWN,
		/// <summary>
		/// Constant mouse button up.
		/// </summary>
        MouseButtonUp = SdlSystem.SDL_EventType.SDL_MOUSEBUTTONUP,
		/// <summary>
		/// Constant joystick axis motion.
		/// </summary>
        JoystickAxisMotion = SdlSystem.SDL_EventType.SDL_JOYAXISMOTION,
		/// <summary>
		/// Constant joystick ball motion.
		/// </summary>
        JoystickBallMotion = SdlSystem.SDL_EventType.SDL_JOYBALLMOTION,
		/// <summary>
		/// Constant joystick hat motion.
		/// </summary>
        JoystickHatMotion = SdlSystem.SDL_EventType.SDL_JOYHATMOTION,
		/// <summary>
		/// Constant joystick button down.
		/// </summary>
        JoystickButtonDown = SdlSystem.SDL_EventType.SDL_JOYBUTTONDOWN,
		/// <summary>
		/// Constant joystick button up.
		/// </summary>
        JoystickButtonUp = SdlSystem.SDL_EventType.SDL_JOYBUTTONUP,
		/// <summary>
		/// Constant video resize.
		/// </summary>
        VideoResize = SdlSystem.SDL_EventType.SDL_VIDEORESIZE,
		/// <summary>
		/// Constant video expose.
		/// </summary>
        VideoExpose = SdlSystem.SDL_EventType.SDL_VIDEOEXPOSE,
		/// <summary>
		/// Constant quit.
		/// </summary>
        Quit = SdlSystem.SDL_EventType.SDL_QUIT,
		/// <summary>
		/// Constant window manager event.
		/// </summary>
        WindowManagerEvent = SdlSystem.SDL_EventType.SDL_SYSWMEVENT,
		/// <summary>
		/// Constant user event.
		/// </summary>
        UserEvent = SdlSystem.SDL_EventType.SDL_USEREVENT
    }


}
