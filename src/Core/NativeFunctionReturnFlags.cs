// 
//  NativeFunctionReturnFlags.cs
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
	/// Native function return flags.
	/// </summary>
    internal enum NativeFunctionReturnFlags : int {
		/// <summary>
		/// Constant error.
		/// </summary>
        Error = -1,
		/// <summary>
		/// Constant success.
		/// </summary>
        Success = 0,
		/// <summary>
		/// Constant infinite loop.
		/// </summary>
        InfiniteLoop = -1,
		/// <summary>
		/// Constant error2.
		/// </summary>
        Error2 = 1,
		/// <summary>
		/// Constant success2.
		/// </summary>
        Success2 = 1,
		/// <summary>
		/// Constant none.
		/// </summary>
        None = 0,
		/// <summary>
		/// Constant first free channel.
		/// </summary>
        FirstFreeChannel = -1,
		/// <summary>
		/// Constant true value.
		/// </summary>
        TrueValue = 1,
		/// <summary>
		/// Constant false value.
		/// </summary>
        FalseValue = 0
    }


}
