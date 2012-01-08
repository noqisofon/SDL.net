// 
//  MouseButtonEventArgs.cs
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


namespace Sdl.Input {
	using Sdl.Core;
	
	
	/// <summary>
	/// Mouse button event arguments.
	/// </summary>
	public class MouseButtonEventArgs: SdlEventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Input.MouseButtonEventArgs"/> class.
		/// </summary>
		public MouseButtonEventArgs() {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Input.MouseButtonEventArgs"/> class.
		/// </summary>
		/// <param name='ev'>
		/// Ev.
		/// </param>
		internal MouseButtonEventArgs(SdlSystem.SDL_Event ev) : base(ev) {
		}
		
		
		/// <summary>
		/// Creates the event arguments.
		/// </summary>
		/// <returns>
		/// The event arguments.
		/// </returns>
		/// <param name='e'>
		/// The ${ParameterType} instance containing the event data.
		/// </param>
		/// <exception cref='NotImplementedException'>
		/// Is thrown when a requested operation is not implemented for a given type.
		/// </exception>
		public static new MouseButtonEventArgs CreateEventArgs(SdlSystem. SDL_Event e) {
			return new MouseButtonEventArgs( e );
		}
	}


}

