// 
//  SdlException.cs
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


namespace Sdl.Core {
	
	
	using Sdl.Graphics;

	
	/// <summary>
	/// Sdl exception.
	/// </summary>
    public class SdlException : Exception {
#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.SdlException"/> class.
		/// </summary>
        public SdlException() {
            throw SdlException.Generate();
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.SdlException"/> class.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
        public SdlException(string message) : base ( message ) {
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.SdlException"/> class.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='exception'>
		/// Exception.
		/// </param>
        public SdlException(string message, Exception exception) : base ( message, exception ) {
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.SdlException"/> class.
		/// </summary>
		/// <param name='info'>
		/// Info.
		/// </param>
		/// <param name='context'>
		/// Context.
		/// </param>
        protected SdlException(SerializationInfo info, StreamingContext context) : base( info, context ) {
        }
#endregion


#region Public Methods
		/// <summary>
		/// Generate this instance.
		/// </summary>
        public static SdlException Generate() {
            string message = SdlSystem.SDL_GetError();

            if ( message.IndexOf( "Surface was lost" ) == -1 )
                return new SdlException( message );
            else
                return new SurfaceLostException( message );
        }
#endregion
    }


}
