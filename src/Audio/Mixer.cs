// 
//  Mixer.cs
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


namespace Sdl.Audio {
	
	
	/// <summary>
	/// Mixer.
	/// </summary>
	public class Mixer {
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Audio.Mixer"/> class.
		/// </summary>
		public Mixer() {
		}
		
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Sdl.Audio.Mixer"/> audio open.
		/// </summary>
		/// <value>
		/// <c>true</c> if audio open; otherwise, <c>false</c>.
		/// </value>
		public static bool AudioOpen {
			get { return __audio_open; }
			set { __audio_open = value; }
		}
		
		
		/// <summary>
		/// Sets a value indicating whether this <see cref="Sdl.Audio.Mixer"/> audio locked.
		/// </summary>
		/// <value>
		/// <c>true</c> if audio locked; otherwise, <c>false</c>.
		/// </value>
		public static bool AudioLocked {
			get { return __audio_locked; }
			set { __audio_locked = value; }
		}

	
		private static bool __audio_open = false;
		private static bool __audio_locked = false;
	}
 

}

