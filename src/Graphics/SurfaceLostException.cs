// 
//  SurfaceLostException.cs
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
using System.Runtime.Serialization;


namespace Sdl.Graphics {
	
	
	using Sdl.Core;
	
	
	/// <summary>
	/// Surface lost exception.
	/// </summary>
	public class SurfaceLostException : SdlException {
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Graphics.SurfaceLostException"/> class.
		/// </summary>
		public SurfaceLostException() {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Graphics.SurfaceLostException"/> class.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		public SurfaceLostException(string message) : base ( message ) {
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Graphics.SurfaceLostException"/> class.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='exception'>
		/// Exception.
		/// </param>
        public SurfaceLostException(string message, Exception exception) : base ( message, exception ) {
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Graphics.SurfaceLostException"/> class.
		/// </summary>
		/// <param name='info'>
		/// Info.
		/// </param>
		/// <param name='context'>
		/// Context.
		/// </param>
        protected SurfaceLostException(SerializationInfo info, StreamingContext context) : base( info, context ) {
        }
	}


}

