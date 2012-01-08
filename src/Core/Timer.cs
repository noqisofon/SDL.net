// 
//  Timer.cs
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
	/// Timer.
	/// </summary>
	/// <exception cref='OverflowException'>
	/// Is thrown when the result of an arithmetic operation is too large to be represented by the destination type.
	/// </exception>
    public static class Timer {
#region Public Properties
		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
		/// </value>
        public static bool IsInitialized {
            get { return __is_initialized; }
        }

		
		/// <summary>
		/// Gets the tick elapsed.
		/// </summary>
		/// <value>
		/// The tick elapsed.
		/// </value>
        public static int TickElapsed {
            get { return (int)SdlSystem.SDL_GetTicks(); }
        }

		
		/// <summary>
		/// Gets the seconds elapsed.
		/// </summary>
		/// <value>
		/// The seconds elapsed.
		/// </value>
        public static double SecondsElapsed {
            get { return TickElapsed / 1000.0; }
        }
#endregion


#region Public Methods
		/// <summary>
		/// Initialize this instance.
		/// </summary>
        public static bool Initialize() {
            if ( SdlSystem.SDL_WasInit( SdlSystem.SDL_INIT_TIMER ) == (int)NativeFunctionReturnFlags.FalseValue ) {
                if ( SdlSystem.SDL_Init( SdlSystem.SDL_INIT_TIMER ) == (int)NativeFunctionReturnFlags.Success )
                    return true;
                else
                    throw SdlException.Generate();
            } else
                return true;
        }

		
		/// <summary>
		/// Delaies the ticks.
		/// </summary>
		/// <param name='delay_time'>
		/// Delay_time.
		/// </param>
        public static void DelayTicks(uint delay_time) {
            SdlSystem.SDL_Delay( delay_time );
        }

		
		/// <summary>
		/// Delaies the seconds.
		/// </summary>
		/// <param name='delay_time'>
		/// Delay_time.
		/// </param>
		/// <exception cref='OverflowException'>
		/// Is thrown when the result of an arithmetic operation is too large to be represented by the destination type.
		/// </exception>
        public static void DelaySeconds(int delay_time) {
            int delay_time_max = int.MaxValue / 1000;
            int delay_time_temp = delay_time * 1000;

            if ( delay_time_temp <= delay_time_max )
                SdlSystem.SDL_Delay( (uint)delay_time );
            else
                throw new OverflowException();
        }

		
		/// <summary>
		/// Frameses to seconds.
		/// </summary>
		/// <returns>
		/// The to seconds.
		/// </returns>
		/// <param name='frames'>
		/// Frames.
		/// </param>
        public static double FramesToSeconds(int frames) {
            return (double)frames / (double)SdlSystem.CD_FPS;
        }

		
		/// <summary>
		/// Secondses to frames.
		/// </summary>
		/// <returns>
		/// The to frames.
		/// </returns>
		/// <param name='seconds'>
		/// Seconds.
		/// </param>
		/// <exception cref='OverflowException'>
		/// Is thrown when the result of an arithmetic operation is too large to be represented by the destination type.
		/// </exception>
        public static int SecondsToFrames(int seconds) {
            if ( seconds <= ( int.MaxValue / SdlSystem.CD_FPS ) )
                return seconds * SdlSystem.CD_FPS;
            else
                throw new OverflowException();
        }

		
		/// <summary>
		/// Frameses to time.
		/// </summary>
		/// <returns>
		/// The to time.
		/// </returns>
		/// <param name='frames'>
		/// Frames.
		/// </param>
        public static TimeSpan FramesToTime(int frames) {
            return new TimeSpan( (long)FramesToSeconds( frames ) * TimeSpan.TicksPerSecond );
        }

		
		/// <summary>
		/// Secondses to time.
		/// </summary>
		/// <returns>
		/// The to time.
		/// </returns>
		/// <param name='seconds'>
		/// Seconds.
		/// </param>
        public static TimeSpan SecondsToTime(int seconds) {
            return new TimeSpan( seconds * TimeSpan.TicksPerSecond );
        }

		
		/// <summary>
		/// Close this instance.
		/// </summary>
        public static void Close() {
            if ( SdlSystem.SDL_WasInit( SdlSystem.SDL_INIT_TIMER ) != 0 )
                SdlSystem.SDL_QuitSubSystem( SdlSystem.SDL_INIT_TIMER );
        }
#endregion

        
#region Private fields
        private static bool __is_initialized = Initialize();
#endregion
    }


}
