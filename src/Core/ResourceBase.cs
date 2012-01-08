// 
//  ResourceBase.cs
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
using System.Runtime.InteropServices;


namespace Sdl.Core {

	
	/// <summary>
	/// Resource base.
	/// </summary>
    public abstract class ResourceBase : IDisposable {
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.ResourceBase"/> class.
		/// </summary>
        protected ResourceBase() : this( IntPtr.Zero ) {
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Sdl.Core.ResourceBase"/> class.
		/// </summary>
		/// <param name='handle'>
		/// Handle.
		/// </param>
        protected ResourceBase(IntPtr handle) {
            this.disposed_ = false;
            this.handle_ = handle;
        }

		
		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="Sdl.Core.ResourceBase"/>
		/// is reclaimed by garbage collection.
		/// </summary>
        ~ResourceBase() {
            this.Dispose( false );
        }

		
		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
		/// </value>
        public bool IsDisposed {
            get { return this.disposed_; }
        }

		
		/// <summary>
		/// Gets or sets the handle.
		/// </summary>
		/// <value>
		/// The handle.
		/// </value>
        public IntPtr Handle {
            get { return this.handle_; }
            set {
                this.handle_ = value;
                GC.KeepAlive( this );
            }
        }

		
		/// <summary>
		/// Releases all resource used by the <see cref="Sdl.Core.ResourceBase"/> object.
		/// </summary>
		/// <remarks>
		/// Call <see cref="Dispose"/> when you are finished using the <see cref="Sdl.Core.ResourceBase"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Sdl.Core.ResourceBase"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="Sdl.Core.ResourceBase"/> so the garbage
		/// collector can reclaim the memory that the <see cref="Sdl.Core.ResourceBase"/> was occupying.
		/// </remarks>
        public void Dispose() {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }

		
		/// <summary>
		/// Close this instance.
		/// </summary>
        public void Close() {
            this.Dispose();
        }

		
		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name='disposing'>
		/// Disposing.
		/// </param>
        protected virtual void Dispose(bool disposing) {
            if ( !this.disposed_ ) {
                if ( disposing ) {
				}
                this.CloseHandle();
            }
            this.disposed_ = true;
        }

		
		/// <summary>
		/// Closes the handle.
		/// </summary>
        protected abstract void CloseHandle();
        

        private bool disposed_;
        private IntPtr handle_;
    }
    
}
// Local Variables:
//   coding: utf-8
// End: 
