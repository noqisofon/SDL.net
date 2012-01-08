// 
//  EventMask.cs
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


namespace Sdl.Core {

	
	/// <summary>
	/// Event mask.
	/// </summary>
    public enum EventMask : uint {
		/// <summary>
		/// Constant none.
		/// </summary>
        None = 0,
		/// <summary>
		/// Constant active event.
		/// </summary>
        ActiveEvent = SdlSystem.SDL_EventMask.SDL_ACTIVEEVENTMASK,
		/// <summary>
		/// Constant key down.
		/// </summary>
        KeyDown = SdlSystem.SDL_EventMask.SDL_KEYDOWNMASK,
		/// <summary>
		/// Constant key up.
		/// </summary>
        KeyUp = SdlSystem.SDL_EventMask.SDL_KEYUPMASK,
		/// <summary>
		/// Constant key event.
		/// </summary>
        KeyEvent = SdlSystem.SDL_EventMask.SDL_KEYEVENTMASK,
		/// <summary>
		/// Constant mouse motion.
		/// </summary>
        MouseMotion = SdlSystem.SDL_EventMask.SDL_MOUSEMOTIONMASK,
		/// <summary>
		/// Constant mouse button down.
		/// </summary>
        MouseButtonDown = SdlSystem.SDL_EventMask.SDL_MOUSEBUTTONDOWNMASK,
		/// <summary>
		/// Constant mouse button up.
		/// </summary>
        MouseButtonUp = SdlSystem.SDL_EventMask.SDL_MOUSEBUTTONUPMASK,
		/// <summary>
		/// Constant mouse event.
		/// </summary>
        MouseEvent = SdlSystem.SDL_EventMask.SDL_MOUSEEVENTMASK,
		/// <summary>
		/// Constant joystick axis motion.
		/// </summary>
        JoystickAxisMotion = SdlSystem.SDL_EventMask.SDL_JOYAXISMOTIONMASK,
		/// <summary>
		/// Constant joystick ball motion.
		/// </summary>
        JoystickBallMotion = SdlSystem.SDL_EventMask.SDL_JOYBALLMOTIONMASK,
		/// <summary>
		/// Constant joystick hat motion.
		/// </summary>
        JoystickHatMotion = SdlSystem.SDL_EventMask.SDL_JOYHATMOTIONMASK,
		/// <summary>
		/// Constant joystick button down.
		/// </summary>
        JoystickButtonDown = SdlSystem.SDL_EventMask.SDL_JOYBUTTONDOWNMASK,
		/// <summary>
		/// Constant joystick button up.
		/// </summary>
        JoystickButtonUp = SdlSystem.SDL_EventMask.SDL_JOYBUTTONUPMASK,
		/// <summary>
		/// Constant joystick event.
		/// </summary>
        JoystickEvent = SdlSystem.SDL_EventMask.SDL_JOYEVENTMASK,
		/// <summary>
		/// Constant video resize.
		/// </summary>
        VideoResize = SdlSystem.SDL_EventMask.SDL_VIDEORESIZEMASK,
		/// <summary>
		/// Constant video expose.
		/// </summary>
        VideoExpose = SdlSystem.SDL_EventMask.SDL_VIDEOEXPOSEMASK,
		/// <summary>
		/// Constant quit.
		/// </summary>
        Quit = SdlSystem.SDL_EventMask.SDL_QUITMASK,
		/// <summary>
		/// Constant window manager event.
		/// </summary>
        WindowManagerEvent = SdlSystem.SDL_EventMask.SDL_SYSWMEVENTMASK,
		/// <summary>
		/// Constant user event.
		/// </summary>
        UserEvent = 1 << SdlSystem.SDL_EventType.SDL_USEREVENT,
		/// <summary>
		/// Constant all events.
		/// </summary>
		AllEvents = SdlSystem.SDL_ALLEVENTS
    }


}
