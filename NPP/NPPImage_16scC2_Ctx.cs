// Copyright (c) 2023, Michael Kunz and Artic Imaging SARL. All rights reserved.
// http://kunzmi.github.io/managedCuda
//
// This file is part of ManagedCuda.
//
// Commercial License Usage
//  Licensees holding valid commercial ManagedCuda licenses may use this
//  file in accordance with the commercial license agreement provided with
//  the Software or, alternatively, in accordance with the terms contained
//  in a written agreement between you and Artic Imaging SARL. For further
//  information contact us at managedcuda@articimaging.eu.
//  
// GNU General Public License Usage
//  Alternatively, this file may be used under the terms of the GNU General
//  Public License as published by the Free Software Foundation, either 
//  version 3 of the License, or (at your option) any later version.
//  
//  ManagedCuda is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see <http://www.gnu.org/licenses/>.


#define ADD_MISSING_CTX
using System;
using System.Diagnostics;

namespace ManagedCuda.NPP
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NPPImage_16scC2 : NPPImageBase
    {
        #region Copy
        /// <summary>
        /// Image copy.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16scC2 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16sc_C2R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Set
        /// <summary>
        /// Set pixel values to nValue.
        /// </summary>
        /// <param name="nValue">Value to be set</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Set(Npp16sc[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MemSet.nppiSet_16sc_C2R_Ctx(nValue, _devPtrRoi, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSet_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region MaxError
        /// <summary>
        /// image maximum error. User buffer is internally allocated and freed.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaxError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.MaximumError.nppiMaximumError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumError_16sc_C2R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image maximum error.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="buffer">Pointer to the user-allocated scratch buffer required for the MaxError operation.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaxError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MaximumError.nppiMaximumError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumError_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for MaxError.
        /// </summary>
        /// <returns></returns>
        public int MaxErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MaximumError.nppiMaximumErrorGetBufferHostSize_16sc_C2R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumErrorGetBufferHostSize_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }
        #endregion

        #region AverageError
        /// <summary>
        /// image average error. User buffer is internally allocated and freed.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void AverageError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.AverageError.nppiAverageError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageError_16sc_C2R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image average error.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="buffer">Pointer to the user-allocated scratch buffer required for the AverageError operation.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void AverageError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.AverageError.nppiAverageError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageError_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for AverageError.
        /// </summary>
        /// <returns></returns>
        public int AverageErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.AverageError.nppiAverageErrorGetBufferHostSize_16sc_C2R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageErrorGetBufferHostSize_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }
        #endregion

        #region MaximumRelative_Error
        /// <summary>
        /// image maximum relative error. User buffer is internally allocated and freed.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaximumRelativeError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaximumRelativeErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.MaximumRelativeError.nppiMaximumRelativeError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumRelativeError_16sc_C2R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image maximum relative error.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="buffer">Pointer to the user-allocated scratch buffer required for the MaximumRelativeError operation.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaximumRelativeError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaximumRelativeErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MaximumRelativeError.nppiMaximumRelativeError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumRelativeError_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for MaximumRelativeError.
        /// </summary>
        /// <returns></returns>
        public int MaximumRelativeErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MaximumRelativeError.nppiMaximumRelativeErrorGetBufferHostSize_16sc_C2R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumRelativeErrorGetBufferHostSize_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }
        #endregion

        #region AverageRelative_Error
        /// <summary>
        /// image average relative error. User buffer is internally allocated and freed.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void AverageRelativeError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageRelativeErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.AverageRelativeError.nppiAverageRelativeError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageRelativeError_16sc_C2R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image average relative error.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="buffer">Pointer to the user-allocated scratch buffer required for the AverageRelativeError operation.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void AverageRelativeError(NPPImage_16scC2 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageRelativeErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.AverageRelativeError.nppiAverageRelativeError_16sc_C2R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageRelativeError_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for AverageRelativeError.
        /// </summary>
        /// <returns></returns>
        public int AverageRelativeErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.AverageRelativeError.nppiAverageRelativeErrorGetBufferHostSize_16sc_C2R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageRelativeErrorGetBufferHostSize_16sc_C2R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }
        #endregion
    }
}
