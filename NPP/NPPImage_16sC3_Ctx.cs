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
using ManagedCuda.BasicTypes;

namespace ManagedCuda.NPP
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NPPImage_16sC3 : NPPImageBase
    {

        #region Copy
        /// <summary>
        /// Image copy.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="channel">Channel number. This number is added to the dst pointer</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16sC1 dst, int channel, NppStreamContext nppStreamCtx)
        {
            if (channel < 0 | channel >= _channels) throw new ArgumentOutOfRangeException("channel", "channel must be in range [0..2].");
            status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16s_C3C1R_Ctx(_devPtrRoi + channel * _typeSize, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16s_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Three-channel 8-bit unsigned packed to planar image copy.
        /// </summary>
        /// <param name="dst0">Destination image channel 0</param>
        /// <param name="dst1">Destination image channel 1</param>
        /// <param name="dst2">Destination image channel 2</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16sC1 dst0, NPPImage_16sC1 dst1, NPPImage_16sC1 dst2, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] array = new CUdeviceptr[] { dst0.DevicePointerRoi, dst1.DevicePointerRoi, dst2.DevicePointerRoi };
            status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16s_C3P3R_Ctx(_devPtrRoi, _pitch, array, dst0.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16s_C3P3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Three-channel 8-bit unsigned planar to packed image copy.
        /// </summary>
        /// <param name="src0">Source image channel 0</param>
        /// <param name="src1">Source image channel 1</param>
        /// <param name="src2">Source image channel 2</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public static void Copy(NPPImage_16sC1 src0, NPPImage_16sC1 src1, NPPImage_16sC1 src2, NPPImage_16sC3 dest, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] array = new CUdeviceptr[] { src0.DevicePointerRoi, src1.DevicePointerRoi, src2.DevicePointerRoi };
            NppStatus status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16s_P3C3R_Ctx(array, src0.Pitch, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16s_P3C3R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }

        /// <summary>
        /// Image copy.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="channelSrc">Channel number. This number is added to the src pointer</param>
        /// <param name="channelDst">Channel number. This number is added to the dst pointer</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16sC3 dst, int channelSrc, int channelDst, NppStreamContext nppStreamCtx)
        {
            if (channelSrc < 0 | channelSrc >= _channels) throw new ArgumentOutOfRangeException("channelSrc", "channelSrc must be in range [0..2].");
            if (channelDst < 0 | channelDst >= dst.Channels) throw new ArgumentOutOfRangeException("channelDst", "channelDst must be in range [0..2].");
            status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16s_C3CR_Ctx(_devPtrRoi + channelSrc * _typeSize, _pitch, dst.DevicePointerRoi + channelDst * _typeSize, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16s_C3CR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Masked Operation 8-bit unsigned image copy.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="mask">Mask image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16sC3 dst, NPPImage_8uC1 mask, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16s_C3MR_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, mask.DevicePointerRoi, mask.Pitch, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16s_C3MR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Logical
        /// <summary>
        /// image bit shift by constant (right).
        /// </summary>
        /// <param name="nConstant">Constant (Array length = 3)</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void RShiftC(uint[] nConstant, NPPImage_16sC3 dest, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.RightShiftConst.nppiRShiftC_16s_C3R_Ctx(_devPtrRoi, _pitch, nConstant, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiRShiftC_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// image bit shift by constant (right), inplace.
        /// </summary>
        /// <param name="nConstant">Constant (Array length = 3)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void RShiftC(uint[] nConstant, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.RightShiftConst.nppiRShiftC_16s_C3IR_Ctx(nConstant, _devPtrRoi, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiRShiftC_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Add
        /// <summary>
        /// Image addition, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Add(NPPImage_16sC3 src2, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Add.nppiAdd_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAdd_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image addition, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Add(NPPImage_16sC3 src2, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Add.nppiAdd_16s_C3IRSfs_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAdd_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Add constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Values to add</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Add(short[] nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.AddConst.nppiAddC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAddC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Add constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Values to add</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Add(short[] nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.AddConst.nppiAddC_16s_C3IRSfs_Ctx(nConstant, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAddC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Sub
        /// <summary>
        /// Image subtraction, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sub(NPPImage_16sC3 src2, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Sub.nppiSub_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSub_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image subtraction, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sub(NPPImage_16sC3 src2, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Sub.nppiSub_16s_C3IRSfs_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSub_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Subtract constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value to subtract</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sub(short[] nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SubConst.nppiSubC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSubC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Subtract constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value to subtract</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sub(short[] nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SubConst.nppiSubC_16s_C3IRSfs_Ctx(nConstant, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSubC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Mul
        /// <summary>
        /// Image multiplication, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mul(NPPImage_16sC3 src2, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Mul.nppiMul_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMul_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image multiplication, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mul(NPPImage_16sC3 src2, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Mul.nppiMul_16s_C3IRSfs_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMul_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Multiply constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mul(short[] nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MulConst.nppiMulC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMulC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Multiply constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mul(short[] nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MulConst.nppiMulC_16s_C3IRSfs_Ctx(nConstant, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMulC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Div
        /// <summary>
        /// Image division, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(NPPImage_16sC3 src2, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Div.nppiDiv_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDiv_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image division, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(NPPImage_16sC3 src2, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Div.nppiDiv_16s_C3IRSfs_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDiv_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Divide constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(short[] nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.DivConst.nppiDivC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDivC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Divide constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(short[] nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.DivConst.nppiDivC_16s_C3IRSfs_Ctx(nConstant, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDivC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image division, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="dest">Destination image</param>
        /// <param name="rndMode">Result Rounding mode to be used</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(NPPImage_16sC3 src2, NPPImage_16sC3 dest, NppRoundMode rndMode, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.DivRound.nppiDiv_Round_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, rndMode, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDiv_Round_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image division, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="rndMode">Result Rounding mode to be used</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(NPPImage_16sC3 src2, NppRoundMode rndMode, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.DivRound.nppiDiv_Round_16s_C3IRSfs_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, rndMode, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDiv_Round_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Exp
        /// <summary>
        /// Exponential, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Exp(NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Exp.nppiExp_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiExp_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Inplace exponential, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Exp(int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Exp.nppiExp_16s_C3IRSfs_Ctx(_devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiExp_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Ln
        /// <summary>
        /// Natural logarithm, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Ln(NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Ln.nppiLn_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLn_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Natural logarithm, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Ln(int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Ln.nppiLn_16s_C3IRSfs_Ctx(_devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLn_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Sqr
        /// <summary>
        /// Image squared, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sqr(NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Sqr.nppiSqr_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSqr_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Inplace image squared, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sqr(int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Sqr.nppiSqr_16s_C3IRSfs_Ctx(_devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSqr_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Sqrt
        /// <summary>
        /// Image square root, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sqrt(NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Sqrt.nppiSqrt_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSqrt_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Inplace image square root, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sqrt(int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Sqrt.nppiSqrt_16s_C3IRSfs_Ctx(_devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSqrt_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Abs
        /// <summary>
        /// Image absolute value.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Abs(NPPImage_16sC3 dest, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Abs.nppiAbs_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAbs_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image absolute value. In place.
        /// </summary>
        public void Abs(NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Abs.nppiAbs_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAbs_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Convert
        /// <summary>
        /// 16-bit unsigned to 32-bit signed conversion.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Convert(NPPImage_32sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.BitDepthConversion.nppiConvert_16s32s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiConvert_16s32s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// 16-bit unsigned to 8-bit unsigned conversion.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Convert(NPPImage_8uC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.BitDepthConversion.nppiConvert_16s8u_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiConvert_16s8u_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// 16-bit unsigned to 32-bit floating point conversion.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Convert(NPPImage_32fC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.BitDepthConversion.nppiConvert_16s32f_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiConvert_16s32f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Sum
        /// <summary>
        /// Scratch-buffer size for nppiSum_16s_C3R.
        /// </summary>
        /// <returns></returns>
        public int SumGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.Sum.nppiSumGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSumGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image sum with 64-bit double precision result. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="result">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sum(CudaDeviceVariable<double> result, NppStreamContext nppStreamCtx)
        {
            int bufferSize = SumGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.Sum.nppiSum_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, result.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSum_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image sum with 64-bit double precision result. No additional buffer is allocated.
        /// </summary>
        /// <param name="result">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="SumGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sum(CudaDeviceVariable<double> result, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = SumGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.Sum.nppiSum_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, result.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSum_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Min
        /// <summary>
        /// Scratch-buffer size for Min.
        /// </summary>
        /// <returns></returns>
        public int MinGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.Min.nppiMinGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Image pixel minimum. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="min">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Min(CudaDeviceVariable<short> min, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MinGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.Min.nppiMin_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, min.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMin_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image pixel minimum. No additional buffer is allocated.
        /// </summary>
        /// <param name="min">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="MinGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Min(CudaDeviceVariable<short> min, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MinGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.Min.nppiMin_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, min.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMin_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region MinIndex
        /// <summary>
        /// Scratch-buffer size for MinIndex.
        /// </summary>
        /// <returns></returns>
        public int MinIndexGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MinIdx.nppiMinIndxGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinIndxGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Image pixel minimum. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="min">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="indexX">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="indexY">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MinIndex(CudaDeviceVariable<short> min, CudaDeviceVariable<int> indexX, CudaDeviceVariable<int> indexY, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MinIndexGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.MinIdx.nppiMinIndx_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, min.DevicePointer, indexX.DevicePointer, indexY.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinIndx_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image pixel minimum. No additional buffer is allocated.
        /// </summary>
        /// <param name="min">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="indexX">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="indexY">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="MinIndexGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MinIndex(CudaDeviceVariable<short> min, CudaDeviceVariable<int> indexX, CudaDeviceVariable<int> indexY, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MinIndexGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MinIdx.nppiMinIndx_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, min.DevicePointer, indexX.DevicePointer, indexY.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinIndx_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Max
        /// <summary>
        /// Scratch-buffer size for Max.
        /// </summary>
        /// <returns></returns>
        public int MaxGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.Max.nppiMaxGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaxGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Image pixel maximum. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="max">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Max(CudaDeviceVariable<short> max, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.Max.nppiMax_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, max.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMax_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image pixel maximum. No additional buffer is allocated.
        /// </summary>
        /// <param name="max">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="MaxGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Max(CudaDeviceVariable<short> max, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.Max.nppiMax_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, max.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMax_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region MaxIndex
        /// <summary>
        /// Scratch-buffer size for MaxIndex.
        /// </summary>
        /// <returns></returns>
        public int MaxIndexGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MaxIdx.nppiMaxIndxGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaxIndxGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Image pixel maximum. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="max">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="indexX">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="indexY">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaxIndex(CudaDeviceVariable<short> max, CudaDeviceVariable<int> indexX, CudaDeviceVariable<int> indexY, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxIndexGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.MaxIdx.nppiMaxIndx_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, max.DevicePointer, indexX.DevicePointer, indexY.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaxIndx_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image pixel minimum. No additional buffer is allocated.
        /// </summary>
        /// <param name="max">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="indexX">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="indexY">Allocated device memory with size of at least 3 * sizeof(int)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="MaxIndexGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaxIndex(CudaDeviceVariable<short> max, CudaDeviceVariable<int> indexX, CudaDeviceVariable<int> indexY, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxIndexGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MaxIdx.nppiMaxIndx_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, max.DevicePointer, indexX.DevicePointer, indexY.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaxIndx_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region MinMax
        /// <summary>
        /// Scratch-buffer size for MinMax.
        /// </summary>
        /// <returns></returns>
        public int MinMaxGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MinMaxNew.nppiMinMaxGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinMaxGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Image pixel minimum and maximum. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="min">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="max">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MinMax(CudaDeviceVariable<short> min, CudaDeviceVariable<short> max, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MinMaxGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.MinMaxNew.nppiMinMax_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, min.DevicePointer, max.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinMax_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Image pixel minimum and maximum. No additional buffer is allocated.
        /// </summary>
        /// <param name="min">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="max">Allocated device memory with size of at least 3 * sizeof(short)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="MinMaxGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MinMax(CudaDeviceVariable<short> min, CudaDeviceVariable<short> max, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MinMaxGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MinMaxNew.nppiMinMax_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, min.DevicePointer, max.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinMax_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Mean
        /// <summary>
        /// Scratch-buffer size for Mean.
        /// </summary>
        /// <returns></returns>
        public int MeanGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MeanNew.nppiMeanGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMeanGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image mean with 64-bit double precision result. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="mean">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mean(CudaDeviceVariable<double> mean, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MeanGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.MeanNew.nppiMean_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, mean.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMean_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image mean with 64-bit double precision result. No additional buffer is allocated.
        /// </summary>
        /// <param name="mean">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="MeanGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mean(CudaDeviceVariable<double> mean, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MeanGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MeanNew.nppiMean_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, buffer.DevicePointer, mean.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMean_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region NormInf
        /// <summary>
        /// Scratch-buffer size for Norm inf.
        /// </summary>
        /// <returns></returns>
        public int NormInfGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormInf.nppiNormInfGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormInfGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image infinity norm. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="norm">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormInf(CudaDeviceVariable<double> norm, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormInfGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormInf.nppiNorm_Inf_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, norm.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNorm_Inf_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image infinity norm. No additional buffer is allocated.
        /// </summary>
        /// <param name="norm">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormInfGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormInf(CudaDeviceVariable<double> norm, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormInfGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormInf.nppiNorm_Inf_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, norm.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNorm_Inf_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region NormL1
        /// <summary>
        /// Scratch-buffer size for Norm L1.
        /// </summary>
        /// <returns></returns>
        public int NormL1GetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormL1.nppiNormL1GetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormL1GetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image L1 norm. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="norm">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormL1(CudaDeviceVariable<double> norm, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormL1GetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormL1.nppiNorm_L1_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, norm.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNorm_L1_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image L1 norm. No additional buffer is allocated.
        /// </summary>
        /// <param name="norm">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormL1GetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormL1(CudaDeviceVariable<double> norm, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormL1GetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormL1.nppiNorm_L1_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, norm.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNorm_L1_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region NormL2
        /// <summary>
        /// Scratch-buffer size for Norm L2.
        /// </summary>
        /// <returns></returns>
        public int NormL2GetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormL2.nppiNormL2GetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormL2GetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image L2 norm. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="norm">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormL2(CudaDeviceVariable<double> norm, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormL2GetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormL2.nppiNorm_L2_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, norm.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNorm_L2_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image L2 norm. No additional buffer is allocated.
        /// </summary>
        /// <param name="norm">Allocated device memory with size of at least 3 * sizeof(double)</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormL2GetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormL2(CudaDeviceVariable<double> norm, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormL2GetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormL2.nppiNorm_L2_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, norm.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNorm_L2_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Threshold
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations OP the predicate (sourcePixel OP nThreshold) is true, the pixel is set
        /// to nThreshold, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="eComparisonOperation">eComparisonOperation. Only allowed values are <see cref="NppCmpOp.Less"/> and <see cref="NppCmpOp.Greater"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Threshold(NPPImage_16sC3 dest, short[] nThreshold, NppCmpOp eComparisonOperation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThreshold, eComparisonOperation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations OP the predicate (sourcePixel OP nThreshold) is true, the pixel is set
        /// to nThreshold, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="eComparisonOperation">eComparisonOperation. Only allowed values are <see cref="NppCmpOp.Less"/> and <see cref="NppCmpOp.Greater"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Threshold(short[] nThreshold, NppCmpOp eComparisonOperation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThreshold, eComparisonOperation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region ThresholdGT
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations sourcePixel is greater than nThreshold is true, the pixel is set
        /// to nThreshold, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdGT(NPPImage_16sC3 dest, short[] nThreshold, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_GT_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThreshold, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_GT_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations sourcePixel is greater than nThreshold is true, the pixel is set
        /// to nThreshold, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdGT(short[] nThreshold, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_GT_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThreshold, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_GT_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region ThresholdLT
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations sourcePixel is less than nThreshold is true, the pixel is set
        /// to nThreshold, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdLT(NPPImage_16sC3 dest, short[] nThreshold, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_LT_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThreshold, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_LT_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations sourcePixel is less than nThreshold is true, the pixel is set
        /// to nThreshold, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdLT(short[] nThreshold, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_LT_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThreshold, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_LT_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region ThresholdVal
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations OP the predicate (sourcePixel OP nThreshold) is true, the pixel is set
        /// to nValue, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="eComparisonOperation">eComparisonOperation. Only allowed values are <see cref="NppCmpOp.Less"/> and <see cref="NppCmpOp.Greater"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Threshold(NPPImage_16sC3 dest, short[] nThreshold, short[] nValue, NppCmpOp eComparisonOperation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_Val_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThreshold, nValue, eComparisonOperation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_Val_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations OP the predicate (sourcePixel OP nThreshold) is true, the pixel is set
        /// to nValue, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="eComparisonOperation">eComparisonOperation. Only allowed values are <see cref="NppCmpOp.Less"/> and <see cref="NppCmpOp.Greater"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Threshold(short[] nThreshold, short[] nValue, NppCmpOp eComparisonOperation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_Val_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThreshold, nValue, eComparisonOperation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_Val_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region ThresholdGTVal
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations sourcePixel is greater than nThreshold is true, the pixel is set
        /// to nValue, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdGT(NPPImage_16sC3 dest, short[] nThreshold, short[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_GTVal_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThreshold, nValue, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_GTVal_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations sourcePixel is greater than nThreshold is true, the pixel is set
        /// to nValue, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdGT(short[] nThreshold, short[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_GTVal_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThreshold, nValue, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_GTVal_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region ThresholdLTVal
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations sourcePixel is less than nThreshold is true, the pixel is set
        /// to nValue, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdLT(NPPImage_16sC3 dest, short[] nThreshold, short[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_LTVal_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThreshold, nValue, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_LTVal_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations sourcePixel is less than nThreshold is true, the pixel is set
        /// to nValue, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdLT(short[] nThreshold, short[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_LTVal_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThreshold, nValue, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_LTVal_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region ThresholdLTValGTVal
        /// <summary>
        /// Image threshold.<para/>
        /// If for a comparison operations sourcePixel is less than nThresholdLT is true, the pixel is set
        /// to nValueLT, else if sourcePixel is greater than nThresholdGT the pixel is set to nValueGT, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThresholdLT">The thresholdLT value.</param>
        /// <param name="nValueLT">The thresholdLT replacement value.</param>
        /// <param name="nThresholdGT">The thresholdGT value.</param>
        /// <param name="nValueGT">The thresholdGT replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdLTGT(NPPImage_16sC3 dest, short[] nThresholdLT, short[] nValueLT, short[] nThresholdGT, short[] nValueGT, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_LTValGTVal_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nThresholdLT, nValueLT, nThresholdGT, nValueGT, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_LTValGTVal_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place image threshold.<para/>
        /// If for a comparison operations sourcePixel is less than nThresholdLT is true, the pixel is set
        /// to nValueLT, else if sourcePixel is greater than nThresholdGT the pixel is set to nValueGT, otherwise it is set to sourcePixel.
        /// </summary>
        /// <param name="nThresholdLT">The thresholdLT value.</param>
        /// <param name="nValueLT">The thresholdLT replacement value.</param>
        /// <param name="nThresholdGT">The thresholdGT value.</param>
        /// <param name="nValueGT">The thresholdGT replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ThresholdLTGT(short[] nThresholdLT, short[] nValueLT, short[] nThresholdGT, short[] nValueGT, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiThreshold_LTValGTVal_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, nThresholdLT, nValueLT, nThresholdGT, nValueGT, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiThreshold_LTValGTVal_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Compare
        /// <summary>
        /// Compare pSrc1's pixels with corresponding pixels in pSrc2.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="dest">Destination image</param>
        /// <param name="eComparisonOperation">Specifies the comparison operation to be used in the pixel comparison.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Compare(NPPImage_16sC3 src2, NPPImage_8uC1 dest, NppCmpOp eComparisonOperation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Compare.nppiCompare_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, eComparisonOperation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCompare_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Compare pSrc's pixels with constant value.
        /// </summary>
        /// <param name="nConstant">constant value</param>
        /// <param name="dest">Destination image</param>
        /// <param name="eComparisonOperation">Specifies the comparison operation to be used in the pixel comparison.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Compare(short[] nConstant, NPPImage_8uC1 dest, NppCmpOp eComparisonOperation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Compare.nppiCompareC_16s_C3R_Ctx(_devPtrRoi, _pitch, nConstant, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, eComparisonOperation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCompareC_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Histogram
        /// <summary>
        /// Scratch-buffer size for HistogramEven.
        /// </summary>
        /// <param name="nLevels"></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// <returns></returns>
        public int HistogramEvenGetBufferSize(int[] nLevels, NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiHistogramEvenGetBufferSize_16s_C3R_Ctx(_sizeRoi, nLevels, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramEvenGetBufferSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Compute levels with even distribution.
        /// </summary>
        /// <param name="nLevels">The number of levels being computed. nLevels must be at least 2, otherwise an NPP_-
        /// HISTO_NUMBER_OF_LEVELS_ERROR error is returned.</param>
        /// <param name="nLowerBound">Lower boundary value of the lowest level.</param>
        /// <param name="nUpperBound">Upper boundary value of the greatest level.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// <returns>An array of size nLevels which receives the levels being computed.</returns>
        public int[] EvenLevels(int nLevels, int nLowerBound, int nUpperBound, NppStreamContext nppStreamCtx)
        {
            int[] Levels = new int[nLevels];
            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiEvenLevelsHost_32s_Ctx(Levels, nLevels, nLowerBound, nUpperBound, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiEvenLevelsHost_32s_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return Levels;
        }

        /// <summary>
        /// Histogram with evenly distributed bins. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="histogram">Allocated device memory of size nLevels (3 Variables)</param>
        /// <param name="nLowerLevel">Lower boundary of lowest level bin. E.g. 0 for [0..255]. Size = 3</param>
        /// <param name="nUpperLevel">Upper boundary of highest level bin. E.g. 256 for [0..255]. Size = 3</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void HistogramEven(CudaDeviceVariable<int>[] histogram, int[] nLowerLevel, int[] nUpperLevel, NppStreamContext nppStreamCtx)
        {
            int[] size = new int[] { histogram[0].Size + 1, histogram[1].Size + 1, histogram[2].Size + 1 };
            CUdeviceptr[] devPtrs = new CUdeviceptr[] { histogram[0].DevicePointer, histogram[1].DevicePointer, histogram[2].DevicePointer };


            int bufferSize = HistogramEvenGetBufferSize(size, nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiHistogramEven_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, devPtrs, size, nLowerLevel, nUpperLevel, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramEven_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Histogram with evenly distributed bins. No additional buffer is allocated.
        /// </summary>
        /// <param name="histogram">Allocated device memory of size nLevels (3 Variables)</param>
        /// <param name="nLowerLevel">Lower boundary of lowest level bin. E.g. 0 for [0..255]. Size = 3</param>
        /// <param name="nUpperLevel">Upper boundary of highest level bin. E.g. 256 for [0..255]. Size = 3</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="HistogramEvenGetBufferSize(int[])"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void HistogramEven(CudaDeviceVariable<int>[] histogram, int[] nLowerLevel, int[] nUpperLevel, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int[] size = new int[] { histogram[0].Size + 1, histogram[1].Size + 1, histogram[2].Size + 1 };
            CUdeviceptr[] devPtrs = new CUdeviceptr[] { histogram[0].DevicePointer, histogram[1].DevicePointer, histogram[2].DevicePointer };

            int bufferSize = HistogramEvenGetBufferSize(size, nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiHistogramEven_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, devPtrs, size, nLowerLevel, nUpperLevel, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramEven_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Scratch-buffer size for HistogramRange.
        /// </summary>
        /// <param name="nLevels"></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// <returns></returns>
        public int HistogramRangeGetBufferSize(int[] nLevels, NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiHistogramRangeGetBufferSize_16s_C3R_Ctx(_sizeRoi, nLevels, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramRangeGetBufferSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Histogram with bins determined by pLevels array. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="histogram">array that receives the computed histogram. The CudaDeviceVariable must be of size nLevels-1. Array size = 3</param>
        /// <param name="pLevels">Array in device memory containing the level sizes of the bins. The CudaDeviceVariable must be of size nLevels. Array size = 3</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void HistogramRange(CudaDeviceVariable<int>[] histogram, CudaDeviceVariable<int>[] pLevels, NppStreamContext nppStreamCtx)
        {
            int[] size = new int[] { histogram[0].Size, histogram[1].Size, histogram[2].Size };
            CUdeviceptr[] devPtrs = new CUdeviceptr[] { histogram[0].DevicePointer, histogram[1].DevicePointer, histogram[2].DevicePointer };
            CUdeviceptr[] devLevels = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };

            int bufferSize = HistogramRangeGetBufferSize(size, nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiHistogramRange_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, devPtrs, devLevels, size, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramRange_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Histogram with bins determined by pLevels array. No additional buffer is allocated.
        /// </summary>
        /// <param name="histogram">array that receives the computed histogram. The CudaDeviceVariable must be of size nLevels-1. Array size = 3</param>
        /// <param name="pLevels">Array in device memory containing the level sizes of the bins. The CudaDeviceVariable must be of size nLevels. Array size = 3</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="HistogramRangeGetBufferSize(int[])"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void HistogramRange(CudaDeviceVariable<int>[] histogram, CudaDeviceVariable<int>[] pLevels, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int[] size = new int[] { histogram[0].Size, histogram[1].Size, histogram[2].Size };
            CUdeviceptr[] devPtrs = new CUdeviceptr[] { histogram[0].DevicePointer, histogram[1].DevicePointer, histogram[2].DevicePointer };
            CUdeviceptr[] devLevels = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };

            int bufferSize = HistogramRangeGetBufferSize(size, nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.Histogram.nppiHistogramRange_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, devPtrs, devLevels, size, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramRange_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        //new in Cuda 5.5
        #region DotProduct
        /// <summary>
        /// Device scratch buffer size (in bytes) for nppiDotProd_16s64f_C3R.
        /// </summary>
        /// <returns></returns>
        public int DotProdGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.DotProd.nppiDotProdGetBufferHostSize_16s64f_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDotProdGetBufferHostSize_16s64f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// Three-channel 16-bit signed image DotProd.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pDp">Pointer to the computed dot product of the two images. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="DotProdGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void DotProduct(NPPImage_16sC3 src2, CudaDeviceVariable<double> pDp, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = DotProdGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.DotProd.nppiDotProd_16s64f_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pDp.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDotProd_16s64f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Three-channel 16-bit signed image DotProd. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pDp">Pointer to the computed dot product of the two images. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void DotProduct(NPPImage_16sC3 src2, CudaDeviceVariable<double> pDp, NppStreamContext nppStreamCtx)
        {
            int bufferSize = DotProdGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.DotProd.nppiDotProd_16s64f_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pDp.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDotProd_16s64f_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        #endregion

        #region LUT
        /// <summary>
        /// look-up-table color conversion.<para/>
        /// The LUT is derived from a set of user defined mapping points through linear interpolation.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="values0">array of user defined OUTPUT values, channel 0</param>
        /// <param name="levels0">array of user defined INPUT values, channel 0</param>
        /// <param name="values1">array of user defined OUTPUT values, channel 1</param>
        /// <param name="levels1">array of user defined INPUT values, channel 1</param>
        /// <param name="values2">array of user defined OUTPUT values, channel 2</param>
        /// <param name="levels2">array of user defined INPUT values, channel 2</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Lut(NPPImage_16sC3 dest, CudaDeviceVariable<int> values0, CudaDeviceVariable<int> levels0, CudaDeviceVariable<int> values1, CudaDeviceVariable<int> levels1, CudaDeviceVariable<int> values2, CudaDeviceVariable<int> levels2, NppStreamContext nppStreamCtx)
        {
            if (values0.Size != levels0.Size) throw new ArgumentException("values0 and levels0 must have same size.");
            if (values1.Size != levels1.Size) throw new ArgumentException("values1 and levels1 must have same size.");
            if (values2.Size != levels2.Size) throw new ArgumentException("values2 and levels2 must have same size.");

            CUdeviceptr[] values = new CUdeviceptr[3];
            CUdeviceptr[] levels = new CUdeviceptr[3];
            int[] levelLengths = new int[3];

            values[0] = values0.DevicePointer;
            values[1] = values1.DevicePointer;
            values[2] = values2.DevicePointer;
            levels[0] = levels0.DevicePointer;
            levels[1] = levels1.DevicePointer;
            levels[2] = levels2.DevicePointer;

            levelLengths[0] = levels0.Size;
            levelLengths[1] = levels1.Size;
            levelLengths[2] = levels2.Size;

            status = NPPNativeMethods_Ctx.NPPi.ColorLUTLinear.nppiLUT_Linear_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, values, levels, levelLengths, nppStreamCtx);

            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLUT_Linear_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// look-up-table color conversion.
        /// The LUT is derived from a set of user defined mapping points with no interpolation.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="pValues">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined OUTPUT values.</param>
        /// <param name="pLevels">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined INPUT values. pLevels.Size gives nLevels.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void LUT(NPPImage_16sC3 dst, CudaDeviceVariable<int>[] pValues, CudaDeviceVariable<int>[] pLevels, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] ptrsV = new CUdeviceptr[] { pValues[0].DevicePointer, pValues[1].DevicePointer, pValues[2].DevicePointer };
            CUdeviceptr[] ptrsL = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };
            int[] size = new int[] { pLevels[0].Size, pLevels[1].Size, pLevels[2].Size };
            status = NPPNativeMethods_Ctx.NPPi.ColorLUT.nppiLUT_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, ptrsV, ptrsL, size, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLUT_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// cubic interpolated look-up-table color conversion.
        /// The LUT is derived from a set of user defined mapping points through cubic interpolation. 
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="pValues">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined OUTPUT values.</param>
        /// <param name="pLevels">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined INPUT values. pLevels.Size gives nLevels.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void LUTCubic(NPPImage_16sC3 dst, CudaDeviceVariable<int>[] pValues, CudaDeviceVariable<int>[] pLevels, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] ptrsV = new CUdeviceptr[] { pValues[0].DevicePointer, pValues[1].DevicePointer, pValues[2].DevicePointer };
            CUdeviceptr[] ptrsL = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };
            int[] size = new int[] { pLevels[0].Size, pLevels[1].Size, pLevels[2].Size };
            status = NPPNativeMethods_Ctx.NPPi.ColorLUTCubic.nppiLUT_Cubic_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, ptrsV, ptrsL, size, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLUT_Cubic_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// Inplace look-up-table color conversion.
        /// The LUT is derived from a set of user defined mapping points with no interpolation.
        /// </summary>
        /// <param name="pValues">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined OUTPUT values.</param>
        /// <param name="pLevels">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined INPUT values. pLevels.Size gives nLevels.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void LUT(CudaDeviceVariable<int>[] pValues, CudaDeviceVariable<int>[] pLevels, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] ptrsV = new CUdeviceptr[] { pValues[0].DevicePointer, pValues[1].DevicePointer, pValues[2].DevicePointer };
            CUdeviceptr[] ptrsL = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };
            int[] size = new int[] { pLevels[0].Size, pLevels[1].Size, pLevels[2].Size };
            status = NPPNativeMethods_Ctx.NPPi.ColorLUT.nppiLUT_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, ptrsV, ptrsL, size, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLUT_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Inplace cubic interpolated look-up-table color conversion.
        /// The LUT is derived from a set of user defined mapping points through cubic interpolation. 
        /// </summary>
        /// <param name="pValues">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined OUTPUT values.</param>
        /// <param name="pLevels">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined INPUT values. pLevels.Size gives nLevels.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void LUTCubic(CudaDeviceVariable<int>[] pValues, CudaDeviceVariable<int>[] pLevels, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] ptrsV = new CUdeviceptr[] { pValues[0].DevicePointer, pValues[1].DevicePointer, pValues[2].DevicePointer };
            CUdeviceptr[] ptrsL = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };
            int[] size = new int[] { pLevels[0].Size, pLevels[1].Size, pLevels[2].Size };
            status = NPPNativeMethods_Ctx.NPPi.ColorLUTCubic.nppiLUT_Cubic_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, ptrsV, ptrsL, size, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLUT_Cubic_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Inplace linear interpolated look-up-table color conversion.
        /// The LUT is derived from a set of user defined mapping points through cubic interpolation. 
        /// </summary>
        /// <param name="pValues">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined OUTPUT values.</param>
        /// <param name="pLevels">Host pointer to an array of 3 device memory pointers, one per color CHANNEL, pointing to user defined INPUT values. pLevels.Size gives nLevels.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void LUTLinear(CudaDeviceVariable<int>[] pValues, CudaDeviceVariable<int>[] pLevels, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] ptrsV = new CUdeviceptr[] { pValues[0].DevicePointer, pValues[1].DevicePointer, pValues[2].DevicePointer };
            CUdeviceptr[] ptrsL = new CUdeviceptr[] { pLevels[0].DevicePointer, pLevels[1].DevicePointer, pLevels[2].DevicePointer };
            int[] size = new int[] { pLevels[0].Size, pLevels[1].Size, pLevels[2].Size };
            status = NPPNativeMethods_Ctx.NPPi.ColorLUTLinear.nppiLUT_Linear_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, ptrsV, ptrsL, size, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiLUT_Linear_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        #endregion

        #region Transpose
        /// <summary>
        /// image transpose
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Transpose(NPPImage_16sC3 dest, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Transpose.nppiTranspose_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiTranspose_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Color...New

        /// <summary>
        /// Swap color channels
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="aDstOrder">Integer array describing how channel values are permutated. <para/>The n-th entry of the array
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// contains the number of the channel that is stored in the n-th channel of the output image. <para/>E.g.
        /// Given an RGB image, aDstOrder = [2,1,0] converts this to BGR channel order.</param>
        public void SwapChannels(NPPImage_16sC3 dest, int[] aDstOrder, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SwapChannel.nppiSwapChannels_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, aDstOrder, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSwapChannels_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Swap color channels
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="aDstOrder">Integer array describing how channel values are permutated. <para/>The n-th entry of the array
        /// contains the number of the channel that is stored in the n-th channel of the output image. <para/>E.g.
        /// Given an RGB image, aDstOrder = [3,2,1,0] converts this to VBGR channel order.</param>
        /// <param name="nValue">(V) Single channel constant value that can be replicated in one or more of the 4 destination channels.<para/>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// nValue is either written or not written to a particular channel depending on the aDstOrder entry for that destination
        /// channel. <para/>An aDstOrder value of 3 will output nValue to that channel, an aDstOrder value greater than 3 will leave that
        /// particular destination channel value unmodified.</param>
        public void SwapChannels(NPPImage_16sC4 dest, int[] aDstOrder, short nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SwapChannel.nppiSwapChannels_16s_C3C4R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, aDstOrder, nValue, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSwapChannels_16s_C3C4R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Swap color channels inplace
        /// </summary>
        /// <param name="aDstOrder">Integer array describing how channel values are permutated. <para/>The n-th entry of the array
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// contains the number of the channel that is stored in the n-th channel of the output image. <para/>E.g.
        /// Given an RGB image, aDstOrder = [2,1,0] converts this to BGR channel order.</param>
        public void SwapChannels(int[] aDstOrder, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SwapChannel.nppiSwapChannels_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, aDstOrder, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSwapChannels_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// RGB to Gray conversion
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void RGBToGray(NPPImage_16sC1 dest, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.RGBToGray.nppiRGBToGray_16s_C3C1R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiRGBToGray_16s_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Color to Gray conversion
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="aCoeffs">fixed size array of constant floating point conversion coefficient values, one per color channel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ColorToGray(NPPImage_16sC1 dest, float[] aCoeffs, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.ColorToGray.nppiColorToGray_16s_C3C1R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, aCoeffs, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiColorToGray_16s_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// in place color twist.
        /// 
        /// An input color twist matrix with floating-point coefficient values is applied
        /// within ROI.
        /// </summary>
        /// <param name="aTwist">The color twist matrix with floating-point coefficient values. [3,4]</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ColorTwist(float[,] aTwist, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.ColorTwist.nppiColorTwist32f_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, aTwist, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiColorTwist32f_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// An input color twist matrix with floating-point pixel values is applied
        /// within ROI.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="twistMatrix">The color twist matrix with floating-point pixel values [3,4].</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ColorTwist(NPPImage_16sC3 dest, float[,] twistMatrix, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.ColorTwist.nppiColorTwist32f_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, twistMatrix, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiColorTwist32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Set
        /// <summary>
        /// Set pixel values to nValue.
        /// </summary>
        /// <param name="nValue">Value to be set (Array size = 3)</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Set(short[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MemSet.nppiSet_16s_C3R_Ctx(nValue, _devPtrRoi, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSet_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Set pixel values to nValue. <para/>
        /// The 8-bit mask image affects setting of the respective pixels in the destination image. <para/>
        /// If the mask value is zero (0) the pixel is not set, if the mask is non-zero, the corresponding
        /// destination pixel is set to specified value.
        /// </summary>
        /// <param name="nValue">Value to be set (Array size = 3)</param>
        /// <param name="mask">Mask image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Set(short[] nValue, NPPImage_8uC1 mask, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MemSet.nppiSet_16s_C3MR_Ctx(nValue, _devPtrRoi, _pitch, _sizeRoi, mask.DevicePointerRoi, mask.Pitch, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSet_16s_C3MR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Set pixel values to nValue. <para/>
        /// The 8-bit mask image affects setting of the respective pixels in the destination image. <para/>
        /// If the mask value is zero (0) the pixel is not set, if the mask is non-zero, the corresponding
        /// destination pixel is set to specified value.
        /// </summary>
        /// <param name="nValue">Value to be set</param>
        /// <param name="channel">Channel number. This number is added to the dst pointer</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Set(short nValue, int channel, NppStreamContext nppStreamCtx)
        {
            if (channel < 0 | channel >= _channels) throw new ArgumentOutOfRangeException("channel", "channel must be in range [0..2].");
            status = NPPNativeMethods_Ctx.NPPi.MemSet.nppiSet_16s_C3CR_Ctx(nValue, _devPtrRoi + channel * _typeSize, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSet_16s_C3CR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        #endregion

        #region Copy

        /// <summary>
        /// image copy.
        /// </summary>
        /// <param name="dst">Destination image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MemCopy.nppiCopy_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopy_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// Copy image and pad borders with a constant, user-specifiable color.
        /// </summary>
        /// <param name="dst">Destination image. The image ROI defines the destination region, i.e. the region that gets filled with data from
        /// the source image (inner part) and constant border color (outer part).</param>
        /// <param name="nTopBorderHeight">Height (in pixels) of the top border. The height of the border at the bottom of
        /// the destination ROI is implicitly defined by the size of the source ROI: nBottomBorderHeight =
        /// oDstSizeROI.height - nTopBorderHeight - oSrcSizeROI.height.</param>
        /// <param name="nLeftBorderWidth">Width (in pixels) of the left border. The width of the border at the right side of
        /// the destination ROI is implicitly defined by the size of the source ROI: nRightBorderWidth =
        /// oDstSizeROI.width - nLeftBorderWidth - oSrcSizeROI.width.</param>
        /// <param name="nValue">The pixel value to be set for border pixels.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Copy(NPPImage_16sC3 dst, int nTopBorderHeight, int nLeftBorderWidth, short[] nValue, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.CopyConstBorder.nppiCopyConstBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, nTopBorderHeight, nLeftBorderWidth, nValue, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopyConstBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }



        /// <summary>
        /// image copy with nearest source image pixel color.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nTopBorderHeight">Height (in pixels) of the top border. The height of the border at the bottom of
        /// the destination ROI is implicitly defined by the size of the source ROI: nBottomBorderHeight =
        /// oDstSizeROI.height - nTopBorderHeight - oSrcSizeROI.height.</param>
        /// <param name="nLeftBorderWidth">Width (in pixels) of the left border. The width of the border at the right side of
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// the destination ROI is implicitly defined by the size of the source ROI: nRightBorderWidth =
        /// oDstSizeROI.width - nLeftBorderWidth - oSrcSizeROI.width.</param>
        public void CopyReplicateBorder(NPPImage_16sC3 dst, int nTopBorderHeight, int nLeftBorderWidth, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.CopyReplicateBorder.nppiCopyReplicateBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, nTopBorderHeight, nLeftBorderWidth, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopyReplicateBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// image copy with the borders wrapped by replication of source image pixel colors.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nTopBorderHeight">Height (in pixels) of the top border. The height of the border at the bottom of
        /// the destination ROI is implicitly defined by the size of the source ROI: nBottomBorderHeight =
        /// oDstSizeROI.height - nTopBorderHeight - oSrcSizeROI.height.</param>
        /// <param name="nLeftBorderWidth">Width (in pixels) of the left border. The width of the border at the right side of
        /// <param name="nppStreamCtx">NPP stream context.</param>
        /// the destination ROI is implicitly defined by the size of the source ROI: nRightBorderWidth =
        /// oDstSizeROI.width - nLeftBorderWidth - oSrcSizeROI.width.</param>
        public void CopyWrapBorder(NPPImage_16sC3 dst, int nTopBorderHeight, int nLeftBorderWidth, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.CopyWrapBorder.nppiCopyWrapBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, _sizeRoi, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, nTopBorderHeight, nLeftBorderWidth, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopyWrapBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// linearly interpolated source image subpixel coordinate color copy.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nDx">Fractional part of source image X coordinate.</param>
        /// <param name="nDy">Fractional part of source image Y coordinate.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void CopySubpix(NPPImage_16sC3 dst, float nDx, float nDy, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.CopySubpix.nppiCopySubpix_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nDx, nDy, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiCopySubpix_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Filter
        /// <summary>
        /// Pixels under the mask are multiplied by the respective weights in the mask and the results are summed.<para/>
        /// Before writing the result pixel the sum is scaled back via division by nDivisor.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="aKernelSize">Width and Height of the rectangular kernel.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nDivisor">The factor by which the convolved summation from the Filter operation should be divided. If equal to the sum of coefficients, this will keep the maximum result value within full scale.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Filter(NPPImage_16sC3 dest, CudaDeviceVariable<int> Kernel, NppiSize aKernelSize, NppiPoint oAnchor, int nDivisor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Convolution.nppiFilter_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, Kernel.DevicePointer, aKernelSize, oAnchor, nDivisor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilter_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Apply convolution filter with user specified 1D column of weights. Result pixel is equal to the sum of
        /// the products between the kernel coefficients (pKernel array) and corresponding neighboring column pixel
        /// values in the source image defined by nKernelDim and nAnchorY, divided by nDivisor.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="nKernelSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">Y offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nDivisor">The factor by which the convolved summation from the Filter operation should be divided. If equal to the sum of coefficients, this will keep the maximum result value within full scale.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterColumn(NPPImage_16sC3 dest, CudaDeviceVariable<int> Kernel, int nKernelSize, int nAnchor, int nDivisor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterColumn_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, Kernel.DevicePointer, nKernelSize, nAnchor, nDivisor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterColumn_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Apply general linear Row convolution filter, with rescaling, in a 1D mask region around each source pixel. 
        /// Result pixel is equal to the sum of the products between the kernel
        /// coefficients (pKernel array) and corresponding neighboring row pixel values in the source image defined
        /// by iKernelDim and iAnchorX, divided by iDivisor.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="nKernelSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nDivisor">The factor by which the convolved summation from the Filter operation should be divided. If equal to the sum of coefficients, this will keep the maximum result value within full scale.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRow(NPPImage_16sC3 dest, CudaDeviceVariable<int> Kernel, int nKernelSize, int nAnchor, int nDivisor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterRow_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, Kernel.DevicePointer, nKernelSize, nAnchor, nDivisor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRow_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Apply general linear Row convolution filter, with rescaling, in a 1D mask region around each source pixel with border control. 
        /// Result pixel is equal to the sum of the products between the kernel
        /// coefficients (pKernel array) and corresponding neighboring row pixel values in the source image defined
        /// by iKernelDim and iAnchorX, divided by iDivisor.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="nKernelSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nDivisor">The factor by which the convolved summation from the Filter operation should be divided. If equal to the sum of coefficients, this will keep the maximum result value within full scale.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRowBorder(NPPImage_16sC3 dest, CudaDeviceVariable<int> Kernel, int nKernelSize, int nAnchor, int nDivisor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterRowBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, Kernel.DevicePointer, nKernelSize, nAnchor, nDivisor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRowBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Computes the average pixel values of the pixels under a rectangular mask.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterBox(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.LinearFixedFilters2D.nppiFilterBox_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, oMaskSize, oAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterBox_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Result pixel value is the minimum of pixel values under the rectangular mask region.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMin(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.RankFilters.nppiFilterMin_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, oMaskSize, oAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMin_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Result pixel value is the maximum of pixel values under the rectangular mask region.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMax(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.RankFilters.nppiFilterMax_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, oMaskSize, oAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMax_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// 1D column convolution.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="pKernel">Pointer to the start address of the kernel coefficient array. pKernel.Sizes gives kernel size<para/>
        /// Coefficients are expected to be stored in reverse order.</param>
        /// <param name="nAnchor">Y offset of the kernel origin frame of reference relative to the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterColumn(NPPImage_16sC3 dst, CudaDeviceVariable<float> pKernel, int nAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterColumn32f_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, pKernel.DevicePointer, pKernel.Size, nAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterColumn32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// 1D row convolution.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="pKernel">Pointer to the start address of the kernel coefficient array. pKernel.Sizes gives kernel size<para/>
        /// Coefficients are expected to be stored in reverse order.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference relative to the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRow(NPPImage_16sC3 dst, CudaDeviceVariable<float> pKernel, int nAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterRow32f_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, pKernel.DevicePointer, pKernel.Size, nAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRow32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// convolution filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="pKernel">Pointer to the start address of the kernel coefficient array.<para/>
        /// Coefficients are expected to be stored in reverse order.</param>
        /// <param name="oKernelSize">Width and Height of the rectangular kernel.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Filter(NPPImage_16sC3 dst, CudaDeviceVariable<float> pKernel, NppiSize oKernelSize, NppiPoint oAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.Convolution.nppiFilter32f_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, pKernel.DevicePointer, oKernelSize, oAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilter32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Gauss filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterGauss(NPPImage_16sC3 dst, MaskSize eMaskSize, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterGauss_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, eMaskSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterGauss_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// High pass filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterHighPass(NPPImage_16sC3 dst, MaskSize eMaskSize, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterHighPass_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, eMaskSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterHighPass_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Low pass filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterLowPass(NPPImage_16sC3 dst, MaskSize eMaskSize, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterLowPass_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, eMaskSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterLowPass_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Sharpen filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterSharpen(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterSharpen_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterSharpen_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// horizontal Prewitt filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterPrewittHoriz(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterPrewittHoriz_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterPrewittHoriz_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// vertical Prewitt filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterPrewittVert(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterPrewittVert_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterPrewittVert_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// horizontal Sobel filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void SobelHoriz(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterSobelHoriz_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterSobelHoriz_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// vertical Sobel filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterSobelVert(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterSobelVert_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterSobelVert_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// horizontal Roberts filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRobertsDown(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterRobertsDown_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRobertsDown_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// vertical Roberts filter..
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRobertsUp(NPPImage_16sC3 dst, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterRobertsUp_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRobertsUp_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Laplace filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterLaplace(NPPImage_16sC3 dst, MaskSize eMaskSize, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterLaplace_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, eMaskSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterLaplace_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region NormNew
        /// <summary>
        /// Device scratch buffer size (in bytes) for NormDiff_Inf.
        /// </summary>
        /// <returns></returns>
        public int NormDiffInfGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiffInfGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiffInfGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image NormDiff_Inf.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormDiff">Pointer to the computed Inf-norm of differences. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormDiffInfGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormDiff_Inf(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormDiff, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormDiffInfGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiff_Inf_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormDiff.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiff_Inf_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image NormDiff_Inf. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormDiff">Pointer to the computed Inf-norm of differences. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormDiff_Inf(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormDiff, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormDiffInfGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiff_Inf_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormDiff.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiff_Inf_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// Device scratch buffer size (in bytes) for NormDiff_L1.
        /// </summary>
        /// <returns></returns>
        public int NormDiffL1GetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiffL1GetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiffL1GetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image NormDiff_L1.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormDiff">Pointer to the computed L1-norm of differences. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormDiffL1GetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormDiff_L1(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormDiff, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormDiffL1GetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiff_L1_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormDiff.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiff_L1_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image NormDiff_L1. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormDiff">Pointer to the computed L1-norm of differences. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormDiff_L1(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormDiff, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormDiffL1GetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiff_L1_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormDiff.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiff_L1_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// Device scratch buffer size (in bytes) for NormDiff_L2.
        /// </summary>
        /// <returns></returns>
        public int NormDiffL2GetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiffL2GetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiffL2GetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image NormDiff_L2.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormDiff">Pointer to the computed L2-norm of differences. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormDiffL2GetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormDiff_L2(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormDiff, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormDiffL2GetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiff_L2_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormDiff.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiff_L2_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image NormDiff_L2. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormDiff">Pointer to the computed L2-norm of differences. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormDiff_L2(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormDiff, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormDiffL2GetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormDiff.nppiNormDiff_L2_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormDiff.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormDiff_L2_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }



        /// <summary>
        /// Device scratch buffer size (in bytes) for NormRel_Inf.
        /// </summary>
        /// <returns></returns>
        public int NormRelInfGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRelInfGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRelInfGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image NormRel_Inf.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormRel">Pointer to the computed relative error for the infinity norm of two images. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormRelInfGetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormRel_Inf(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormRel, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormRelInfGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRel_Inf_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormRel.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRel_Inf_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image NormRel_Inf. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormRel">Pointer to the computed relative error for the infinity norm of two images. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormRel_Inf(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormRel, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormRelInfGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRel_Inf_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormRel.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRel_Inf_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// Device scratch buffer size (in bytes) for NormRel_L1.
        /// </summary>
        /// <returns></returns>
        public int NormRelL1GetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRelL1GetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRelL1GetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image NormRel_L1.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormRel">Pointer to the computed relative error for the infinity norm of two images. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormRelL1GetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormRel_L1(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormRel, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormRelL1GetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRel_L1_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormRel.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRel_L1_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image NormRel_L1. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormRel">Pointer to the computed relative error for the infinity norm of two images. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormRel_L1(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormRel, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormRelL1GetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRel_L1_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormRel.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRel_L1_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// Device scratch buffer size (in bytes) for NormRel_L2.
        /// </summary>
        /// <returns></returns>
        public int NormRelL2GetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRelL2GetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRelL2GetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }

        /// <summary>
        /// image NormRel_L2.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormRel">Pointer to the computed relative error for the infinity norm of two images. (3 * sizeof(double))</param>
        /// <param name="buffer">Allocated device memory with size of at <see cref="NormRelL2GetBufferHostSize()"/></param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormRel_L2(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormRel, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormRelL2GetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRel_L2_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormRel.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRel_L2_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image NormRel_L2. Buffer is internally allocated and freed.
        /// </summary>
        /// <param name="tpl">template image.</param>
        /// <param name="pNormRel">Pointer to the computed relative error for the infinity norm of two images. (3 * sizeof(double))</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void NormRel_L2(NPPImage_16sC3 tpl, CudaDeviceVariable<double> pNormRel, NppStreamContext nppStreamCtx)
        {
            int bufferSize = NormRelL2GetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);

            status = NPPNativeMethods_Ctx.NPPi.NormRel.nppiNormRel_L2_16s_C3R_Ctx(_devPtrRoi, _pitch, tpl.DevicePointerRoi, tpl.Pitch, _sizeRoi, pNormRel.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiNormRel_L2_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }


        #endregion

        #region MinMaxEveryNew
        /// <summary>
        /// image MinEvery
        /// </summary>
        /// <param name="src2">Source-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MinEvery(NPPImage_16sC3 src2, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MinMaxEvery.nppiMinEvery_16s_C3IR_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMinEvery_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// image MaxEvery
        /// </summary>
        /// <param name="src2">Source-Image</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaxEvery(NPPImage_16sC3 src2, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MinMaxEvery.nppiMaxEvery_16s_C3IR_Ctx(src2.DevicePointerRoi, src2.Pitch, _devPtrRoi, _pitch, _sizeRoi, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaxEvery_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region MirrorNew


        /// <summary>
        /// Mirror image inplace.
        /// </summary>
        /// <param name="flip">Specifies the axis about which the image is to be mirrored.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mirror(NppiAxis flip, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.GeometricTransforms.nppiMirror_16s_C3IR_Ctx(_devPtrRoi, _pitch, _sizeRoi, flip, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMirror_16s_C3IR_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Mirror image.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="flip">Specifies the axis about which the image is to be mirrored.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mirror(NPPImage_16sC3 dest, NppiAxis flip, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.GeometricTransforms.nppiMirror_16s_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, flip, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMirror_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region GeometryNew

        /// <summary>
        /// image resize.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nXFactor">Factor by which x dimension is changed. </param>
        /// <param name="nYFactor">Factor by which y dimension is changed. </param>
        /// <param name="nXShift">Source pixel shift in x-direction.</param>
        /// <param name="nYShift">Source pixel shift in y-direction.</param>
        /// <param name="eInterpolation">The type of eInterpolation to perform resampling.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void ResizeSqrPixel(NPPImage_16sC3 dst, double nXFactor, double nYFactor, double nXShift, double nYShift, InterpolationMode eInterpolation, NppStreamContext nppStreamCtx)
        {
            NppiRect srcRect = new NppiRect(_pointRoi, _sizeRoi);
            NppiRect dstRect = new NppiRect(dst.PointRoi, dst.SizeRoi);
            status = NPPNativeMethods_Ctx.NPPi.ResizeSqrPixel.nppiResizeSqrPixel_16s_C3R_Ctx(_devPtr, _sizeOriginal, _pitch, srcRect, dst.DevicePointer, dst.Pitch, dstRect, nXFactor, nYFactor, nXShift, nYShift, eInterpolation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiResizeSqrPixel_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// image remap.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="pXMap">Device memory pointer to 2D image array of X coordinate values to be used when sampling source image. </param>
        /// <param name="pYMap">Device memory pointer to 2D image array of Y coordinate values to be used when sampling source image. </param>
        /// <param name="eInterpolation">The type of eInterpolation to perform resampling.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Remap(NPPImage_16sC3 dst, NPPImage_32fC1 pXMap, NPPImage_32fC1 pYMap, InterpolationMode eInterpolation, NppStreamContext nppStreamCtx)
        {
            NppiRect srcRect = new NppiRect(_pointRoi, _sizeRoi);
            status = NPPNativeMethods_Ctx.NPPi.Remap.nppiRemap_16s_C3R_Ctx(_devPtr, _sizeOriginal, _pitch, srcRect, pXMap.DevicePointerRoi, pXMap.Pitch, pYMap.DevicePointerRoi, pYMap.Pitch, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eInterpolation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiRemap_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }



        /// <summary>
        /// image conversion.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="hint">algorithm performance or accuracy selector, currently ignored</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Scale(NPPImage_8uC3 dst, NppHintAlgorithm hint, NppStreamContext nppStreamCtx)
        {
            NppiRect srcRect = new NppiRect(_pointRoi, _sizeRoi);
            status = NPPNativeMethods_Ctx.NPPi.Scale.nppiScale_16s8u_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, hint, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiScale_16s8u_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }




        /// <summary>
        /// planar image resize.
        /// </summary>
        /// <param name="src0">Source image (Channel 0)</param>
        /// <param name="src1">Source image (Channel 1)</param>
        /// <param name="src2">Source image (Channel 2)</param>
        /// <param name="dest0">Destination image (Channel 0)</param>
        /// <param name="dest1">Destination image (Channel 1)</param>
        /// <param name="dest2">Destination image (Channel 2)</param>
        /// <param name="nXFactor">Factor by which x dimension is changed. </param>
        /// <param name="nYFactor">Factor by which y dimension is changed. </param>
        /// <param name="nXShift">Source pixel shift in x-direction.</param>
        /// <param name="nYShift">Source pixel shift in y-direction.</param>
        /// <param name="eInterpolation">The type of eInterpolation to perform resampling.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public static void ResizeSqrPixel(NPPImage_16sC1 src0, NPPImage_16sC1 src1, NPPImage_16sC1 src2, NPPImage_16sC1 dest0, NPPImage_16sC1 dest1, NPPImage_16sC1 dest2, double nXFactor, double nYFactor, double nXShift, double nYShift, InterpolationMode eInterpolation, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] src = new CUdeviceptr[] { src0.DevicePointer, src1.DevicePointer, src2.DevicePointer };
            CUdeviceptr[] dst = new CUdeviceptr[] { dest0.DevicePointer, dest1.DevicePointer, dest2.DevicePointer };
            NppiRect srcRect = new NppiRect(src0.PointRoi, src0.SizeRoi);
            NppiRect dstRect = new NppiRect(dest0.PointRoi, dest0.SizeRoi);
            NppStatus status = NPPNativeMethods_Ctx.NPPi.ResizeSqrPixel.nppiResizeSqrPixel_16s_P3R_Ctx(src, src0.SizeRoi, src0.Pitch, srcRect, dst, dest0.Pitch, dstRect, nXFactor, nYFactor, nXShift, nYShift, eInterpolation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiResizeSqrPixel_16s_P3R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }

        /// <summary>
        /// planar image remap.
        /// </summary>
        /// <param name="src0">Source image (Channel 0)</param>
        /// <param name="src1">Source image (Channel 1)</param>
        /// <param name="src2">Source image (Channel 2)</param>
        /// <param name="dest0">Destination image (Channel 0)</param>
        /// <param name="dest1">Destination image (Channel 1)</param>
        /// <param name="dest2">Destination image (Channel 2)</param>
        /// <param name="pXMap">Device memory pointer to 2D image array of X coordinate values to be used when sampling source image. </param>
        /// <param name="pYMap">Device memory pointer to 2D image array of Y coordinate values to be used when sampling source image. </param>
        /// <param name="eInterpolation">The type of eInterpolation to perform resampling.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public static void Remap(NPPImage_16sC1 src0, NPPImage_16sC1 src1, NPPImage_16sC1 src2, NPPImage_16sC1 dest0, NPPImage_16sC1 dest1, NPPImage_16sC1 dest2, NPPImage_32fC1 pXMap, NPPImage_32fC1 pYMap, InterpolationMode eInterpolation, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] src = new CUdeviceptr[] { src0.DevicePointer, src1.DevicePointer, src2.DevicePointer };
            CUdeviceptr[] dst = new CUdeviceptr[] { dest0.DevicePointerRoi, dest1.DevicePointerRoi, dest2.DevicePointerRoi };
            NppiRect srcRect = new NppiRect(src0.PointRoi, src0.SizeRoi);
            NppStatus status = NPPNativeMethods_Ctx.NPPi.Remap.nppiRemap_16s_P3R_Ctx(src, src0.SizeRoi, src0.Pitch, srcRect, pXMap.DevicePointerRoi, pXMap.Pitch, pYMap.DevicePointerRoi, pYMap.Pitch, dst, dest0.Pitch, dest0.SizeRoi, eInterpolation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiRemap_16s_P3R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }
        #endregion

        #region SwapChannelNew


        /// <summary>
        /// 3 channel planar 8-bit unsigned color twist.
        /// An input color twist matrix with floating-point pixel values is applied
        /// within ROI.
        /// </summary>
        /// <param name="src0">Source image (Channel 0)</param>
        /// <param name="src1">Source image (Channel 1)</param>
        /// <param name="src2">Source image (Channel 2)</param>
        /// <param name="dest0">Destination image (Channel 0)</param>
        /// <param name="dest1">Destination image (Channel 1)</param>
        /// <param name="dest2">Destination image (Channel 2)</param>
        /// <param name="twistMatrix">The color twist matrix with floating-point pixel values [3,4].</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public static void ColorTwist(NPPImage_16sC1 src0, NPPImage_16sC1 src1, NPPImage_16sC1 src2, NPPImage_16sC1 dest0, NPPImage_16sC1 dest1, NPPImage_16sC1 dest2, float[,] twistMatrix, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] src = new CUdeviceptr[] { src0.DevicePointerRoi, src1.DevicePointerRoi, src2.DevicePointerRoi };
            CUdeviceptr[] dst = new CUdeviceptr[] { dest0.DevicePointerRoi, dest1.DevicePointerRoi, dest2.DevicePointerRoi };

            NppStatus status = NPPNativeMethods_Ctx.NPPi.ColorTwist.nppiColorTwist32f_16s_P3R_Ctx(src, src0.Pitch, dst, dest0.Pitch, src0.SizeRoi, twistMatrix, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiColorTwist32f_16s_P3R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }

        /// <summary>
        /// 3 channel planar 8-bit unsigned inplace color twist.
        /// An input color twist matrix with floating-point pixel values is applied
        /// within ROI.
        /// </summary>
        /// <param name="srcDest0">Source / Destination image (Channel 0)</param>
        /// <param name="srcDest1">Source / Destinationimage (Channel 1)</param>
        /// <param name="srcDest2">Source / Destinationimage (Channel 2)</param>
        /// <param name="twistMatrix">The color twist matrix with floating-point pixel values [3,4].</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public static void ColorTwist(NPPImage_16sC1 srcDest0, NPPImage_16sC1 srcDest1, NPPImage_16sC1 srcDest2, float[,] twistMatrix, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] src = new CUdeviceptr[] { srcDest0.DevicePointerRoi, srcDest1.DevicePointerRoi, srcDest2.DevicePointerRoi };

            NppStatus status = NPPNativeMethods_Ctx.NPPi.ColorTwist.nppiColorTwist32f_16s_IP3R_Ctx(src, srcDest0.Pitch, srcDest0.SizeRoi, twistMatrix, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiColorTwist32f_16s_IP3R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }
        #endregion

        //New in Cuda 6.0

        #region SumWindow
        /// <summary>
        /// 16-bit signed 1D (column) sum to 32f.
        /// Apply Column Window Summation filter over a 1D mask region around each
        /// source pixel for 3-channel 16 bit/pixel input images with 32-bit floating point
        /// output.  <para/>
        /// Result 32-bit floating point pixel is equal to the sum of the corresponding and
        /// neighboring column pixel values in a mask region of the source image defined by
        /// nMaskSize and nAnchor. 
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nMaskSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">Y offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void SumWindowColumn(NPPImage_32fC3 dest, int nMaskSize, int nAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.WindowSum1D.nppiSumWindowColumn_16s32f_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nMaskSize, nAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSumWindowColumn_16s32f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// 16-bit signed 1D (row) sum to 32f.<para/>
        /// Apply Row Window Summation filter over a 1D mask region around each source
        /// pixel for 3-channel 16-bit pixel input images with 32-bit floating point output.  
        /// Result 32-bit floating point pixel is equal to the sum of the corresponding and
        /// neighboring row pixel values in a mask region of the source image defined
        /// by nKernelDim and nAnchorX. 
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nMaskSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void SumWindowRow(NPPImage_32fC3 dest, int nMaskSize, int nAnchor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.WindowSum1D.nppiSumWindowRow_16s32f_C3R_Ctx(_devPtrRoi, _pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nMaskSize, nAnchor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSumWindowRow_16s32f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Filter Median
        /// <summary>
        /// Result pixel value is the median of pixel values under the rectangular mask region.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Median operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference relative to the source pixel.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMedian(NPPImage_16sC3 dst, NppiSize oMaskSize, NppiPoint oAnchor, NppStreamContext nppStreamCtx)
        {
            int bufferSize = FilterMedianGetBufferHostSize(oMaskSize, nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.ImageMedianFilter.nppiFilterMedian_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, oMaskSize, oAnchor, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMedian_16s_C3R_Ctx", status));
            buffer.Dispose();
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Result pixel value is the median of pixel values under the rectangular mask region.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Median operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference relative to the source pixel.</param>
        /// <param name="buffer">Pointer to the user-allocated scratch buffer required for the Median operation.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMedian(NPPImage_16sC3 dst, NppiSize oMaskSize, NppiPoint oAnchor, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = FilterMedianGetBufferHostSize(oMaskSize, nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.ImageMedianFilter.nppiFilterMedian_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, oMaskSize, oAnchor, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMedian_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for FilterMedian.
        /// </summary>
        /// <returns></returns>
        public int FilterMedianGetBufferHostSize(NppiSize oMaskSize, NppStreamContext nppStreamCtx)
        {
            uint bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.ImageMedianFilter.nppiFilterMedianGetBufferSize_16s_C3R_Ctx(_sizeRoi, oMaskSize, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMedianGetBufferSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return (int)bufferSize; //We stay consistent with other GetBufferHostSize functions and convert to int.
        }
        #endregion

        #region MaxError
        /// <summary>
        /// image maximum error. User buffer is internally allocated and freed.
        /// </summary>
        /// <param name="src2">2nd source image</param>
        /// <param name="pError">Pointer to the computed error.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void MaxError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.MaximumError.nppiMaximumError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumError_16s_C3R_Ctx", status));
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
        public void MaxError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaxErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MaximumError.nppiMaximumError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumError_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for MaxError.
        /// </summary>
        /// <returns></returns>
        public int MaxErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MaximumError.nppiMaximumErrorGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumErrorGetBufferHostSize_16s_C3R_Ctx", status));
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
        public void AverageError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.AverageError.nppiAverageError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageError_16s_C3R_Ctx", status));
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
        public void AverageError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.AverageError.nppiAverageError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageError_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for AverageError.
        /// </summary>
        /// <returns></returns>
        public int AverageErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.AverageError.nppiAverageErrorGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageErrorGetBufferHostSize_16s_C3R_Ctx", status));
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
        public void MaximumRelativeError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaximumRelativeErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.MaximumRelativeError.nppiMaximumRelativeError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumRelativeError_16s_C3R_Ctx", status));
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
        public void MaximumRelativeError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = MaximumRelativeErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.MaximumRelativeError.nppiMaximumRelativeError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumRelativeError_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for MaximumRelativeError.
        /// </summary>
        /// <returns></returns>
        public int MaximumRelativeErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.MaximumRelativeError.nppiMaximumRelativeErrorGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMaximumRelativeErrorGetBufferHostSize_16s_C3R_Ctx", status));
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
        public void AverageRelativeError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageRelativeErrorGetBufferHostSize(nppStreamCtx);
            CudaDeviceVariable<byte> buffer = new CudaDeviceVariable<byte>(bufferSize);
            status = NPPNativeMethods_Ctx.NPPi.AverageRelativeError.nppiAverageRelativeError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageRelativeError_16s_C3R_Ctx", status));
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
        public void AverageRelativeError(NPPImage_16sC3 src2, CudaDeviceVariable<double> pError, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            int bufferSize = AverageRelativeErrorGetBufferHostSize(nppStreamCtx);
            if (bufferSize > buffer.Size) throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.AverageRelativeError.nppiAverageRelativeError_16s_C3R_Ctx(_devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, pError.DevicePointer, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageRelativeError_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Device scratch buffer size (in bytes) for AverageRelativeError.
        /// </summary>
        /// <returns></returns>
        public int AverageRelativeErrorGetBufferHostSize(NppStreamContext nppStreamCtx)
        {
            int bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.AverageRelativeError.nppiAverageRelativeErrorGetBufferHostSize_16s_C3R_Ctx(_sizeRoi, ref bufferSize, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAverageRelativeErrorGetBufferHostSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }
        #endregion

        #region FilterBorder
        /// <summary>
        /// Three channel 16-bit signed convolution filter with border control.<para/>
        /// General purpose 2D convolution filter with border control.<para/>
        /// Pixels under the mask are multiplied by the respective weights in the mask
        /// and the results are summed. Before writing the result pixel the sum is scaled
        /// back via division by nDivisor. If any portion of the mask overlaps the source
        /// image boundary the requested border type operation is applied to all mask pixels
        /// which fall outside of the source image.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="pKernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order</param>
        /// <param name="nKernelSize">Width and Height of the rectangular kernel.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference relative to the source pixel.</param>
        /// <param name="nDivisor">The factor by which the convolved summation from the Filter operation should be divided.
        /// If equal to the sum of coefficients, this will keep the maximum result value within full scale.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterBorder(NPPImage_16sC3 dest, CudaDeviceVariable<int> pKernel, NppiSize nKernelSize, NppiPoint oAnchor, int nDivisor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterBorder.nppiFilterBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, pKernel.DevicePointer, nKernelSize, oAnchor, nDivisor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Three channel 16-bit signed convolution filter with border control.<para/>
        /// General purpose 2D convolution filter using floating-point weights with border control.<para/>
        /// Pixels under the mask are multiplied by the respective weights in the mask
        /// and the results are summed. Before writing the result pixel the sum is scaled
        /// back via division by nDivisor. If any portion of the mask overlaps the source
        /// image boundary the requested border type operation is applied to all mask pixels
        /// which fall outside of the source image. <para/>
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="pKernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order</param>
        /// <param name="nKernelSize">Width and Height of the rectangular kernel.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference relative to the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterBorder(NPPImage_16sC3 dest, CudaDeviceVariable<float> pKernel, NppiSize nKernelSize, NppiPoint oAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterBorder32f.nppiFilterBorder32f_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, pKernel.DevicePointer, nKernelSize, oAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterBorder32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        #endregion

        #region FilterSobelBorder
        /// <summary>
        /// Filters the image using a horizontal Sobel filter kernel with border control.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterSobelHorizBorder(NPPImage_16sC3 dest, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterSobelHorizBorder.nppiFilterSobelHorizBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterSobelHorizBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Filters the image using a vertical Sobel filter kernel with border control.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterSobelVertBorder(NPPImage_16sC3 dest, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterSobelVertBorder.nppiFilterSobelVertBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterSobelVertBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region FilterGauss
        /// <summary>Filters the image using a Gaussian filter kernel with border control:<para/>
        /// 1/16 2/16 1/16<para/>
        /// 2/16 4/16 2/16<para/>
        /// 1/16 2/16 1/16<para/>
        /// <para/> or <para/>
        /// 2/571 7/571 12/571 7/571 2/571<para/>
        /// 7/571 31/571 52/571 31/571 7/571<para/>
        /// 12/571 52/571 127/571 52/571 12/571<para/>
        /// 7/571 31/571 52/571 31/571 7/571<para/>
        /// 2/571 7/571 12/571 7/571 2/571<para/>
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterGaussBorder(NPPImage_16sC3 dest, MaskSize eMaskSize, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterGaussBorder.nppiFilterGaussBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, eMaskSize, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterGaussBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        //New in Cuda 7.0
        #region FilterColumnBorder
        /// <summary>
        /// General purpose 1D convolution column filter with border control.<para/>
        /// Pixels under the mask are multiplied by the respective weights in the mask
        /// and the results are summed. Before writing the result pixel the sum is scaled
        /// back via division by nDivisor. If any portion of the mask overlaps the source
        /// image boundary the requested border type operation is applied to all mask pixels
        /// which fall outside of the source image.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="nDivisor">The factor by which the convolved summation from the Filter operation should be divided. If equal to the sum of coefficients, this will keep the maximum result value within full scale.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterColumnBorder(NPPImage_16sC3 dest, CudaDeviceVariable<int> Kernel, int nAnchor, int nDivisor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterColumnBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, Kernel.DevicePointer, Kernel.Size, nAnchor, nDivisor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterColumnBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// General purpose 1D convolution column filter with border control.<para/>
        /// Pixels under the mask are multiplied by the respective weights in the mask
        /// and the results are summed. If any portion of the mask overlaps the source
        /// image boundary the requested border type operation is applied to all mask pixels
        /// which fall outside of the source image.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterColumnBorder(NPPImage_16sC3 dest, CudaDeviceVariable<float> Kernel, int nAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterColumnBorder32f_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, Kernel.DevicePointer, Kernel.Size, nAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterColumnBorder32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region FilterRow
        /// <summary>
        /// General purpose 1D convolution row filter with border control.<para/>
        /// Pixels under the mask are multiplied by the respective weights in the mask
        /// and the results are summed. If any portion of the mask overlaps the source
        /// image boundary the requested border type operation is applied to all mask pixels
        /// which fall outside of the source image.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="Kernel">Pointer to the start address of the kernel coefficient array. Coeffcients are expected to be stored in reverse order.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRowBorder(NPPImage_16sC3 dest, CudaDeviceVariable<float> Kernel, int nAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.LinearFilter1D.nppiFilterRowBorder32f_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, Kernel.DevicePointer, Kernel.Size, nAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRowBorder32f_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        #endregion

        #region SumWindow

        /// <summary>
        /// Apply Column Window Summation filter over a 1D mask region around each
        /// source pixel for 3-channel 8 bit/pixel input images with 32-bit floating point
        /// output.  
        /// Result 32-bit floating point pixel is equal to the sum of the corresponding and
        /// neighboring column pixel values in a mask region of the source image defined by
        /// nMaskSize and nAnchor. 
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nMaskSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">Y offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void SumWindowColumnBorder(NPPImage_32fC3 dest, int nMaskSize, int nAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.WindowSum1D.nppiSumWindowColumnBorder_16s32f_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, nMaskSize, nAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSumWindowColumnBorder_16s32f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Apply Row Window Summation filter over a 1D mask region around each source
        /// pixel for 3-channel 8-bit pixel input images with 32-bit floating point output.  
        /// Result 32-bit floating point pixel is equal to the sum of the corresponding and
        /// neighboring row pixel values in a mask region of the source image defined
        /// by nKernelDim and nAnchorX. 
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nMaskSize">Length of the linear kernel array.</param>
        /// <param name="nAnchor">X offset of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void SumWindowRowBorder(NPPImage_32fC3 dest, int nMaskSize, int nAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.WindowSum1D.nppiSumWindowRowBorder_16s32f_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, nMaskSize, nAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSumWindowRowBorder_16s32f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region FilterBox


        /// <summary>
        /// Computes the average pixel values of the pixels under a rectangular mask.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterBoxBorder(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.LinearFixedFilters2D.nppiFilterBoxBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, oMaskSize, oAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterBoxBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        #endregion

        #region Filter Min/Max


        /// <summary>
        /// Result pixel value is the minimum of pixel values under the rectangular mask region.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMinBorder(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.RankFilters.nppiFilterMinBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, oMaskSize, oAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMinBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Result pixel value is the maximum of pixel values under the rectangular mask region.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMaxBorder(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.RankFilters.nppiFilterMaxBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, oMaskSize, oAnchor, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMaxBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region FilterOthers


        /// <summary>
        /// horizontal Prewitt filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterPrewittHorizBorder(NPPImage_16sC3 dst, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterPrewittHorizBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterPrewittHorizBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// vertical Prewitt filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterPrewittVertBorder(NPPImage_16sC3 dst, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterPrewittVertBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterPrewittVertBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// horizontal Roberts filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRobertsDownBorder(NPPImage_16sC3 dst, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterRobertsDownBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRobertsDownBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// vertical Roberts filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterRobertsUpBorder(NPPImage_16sC3 dst, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterRobertsUpBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterRobertsUpBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Laplace filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterLaplaceBorder(NPPImage_16sC3 dst, MaskSize eMaskSize, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterLaplaceBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eMaskSize, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterLaplaceBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// High pass filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterHighPassBorder(NPPImage_16sC3 dst, MaskSize eMaskSize, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterHighPassBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eMaskSize, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterHighPassBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Low pass filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eMaskSize">Enumeration value specifying the mask size.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterLowPassBorder(NPPImage_16sC3 dst, MaskSize eMaskSize, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterLowPassBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eMaskSize, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterLowPassBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Sharpen filter.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterSharpenBorder(NPPImage_16sC3 dst, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterSharpenBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterSharpenBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion
        #region Filter Unsharp

        /// <summary>
        /// Filters the image using a unsharp-mask sharpening filter kernel with border control.<para/>
        /// The algorithm involves the following steps:<para/>
        /// Smooth the original image with a Gaussian filter, with the width controlled by the nRadius.<para/>
        /// Subtract the smoothed image from the original to create a high-pass filtered image.<para/>
        /// Apply any clipping needed on the high-pass image, as controlled by the nThreshold.<para/>
        /// Add a certain percentage of the high-pass filtered image to the original image, 
        /// with the percentage controlled by the nWeight.
        /// In pseudocode this algorithm can be written as:<para/>
        /// HighPass = Image - Gaussian(Image)<para/>
        /// Result = Image + nWeight * HighPass * ( |HighPass| >= nThreshold ) <para/>
        /// where nWeight is the amount, nThreshold is the threshold, and >= indicates a Boolean operation, 1 if true, or 0 otherwise.
        /// <para/>
        /// If any portion of the mask overlaps the source image boundary, the requested border type 
        /// operation is applied to all mask pixels which fall outside of the source image.
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="nRadius">The radius of the Gaussian filter, in pixles, not counting the center pixel.</param>
        /// <param name="nSigma">The standard deviation of the Gaussian filter, in pixel.</param>
        /// <param name="nWeight">The percentage of the difference between the original and the high pass image that is added back into the original.</param>
        /// <param name="nThreshold">The threshold needed to apply the difference amount.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="buffer">Pointer to the user-allocated device scratch buffer required for the unsharp operation.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterUnsharpBorder(NPPImage_16sC3 dst, float nRadius, float nSigma, float nWeight, float nThreshold, NppiBorderType eBorderType, CudaDeviceVariable<byte> buffer, NppStreamContext nppStreamCtx)
        {
            if (buffer.Size < FilterUnsharpGetBufferSize(nRadius, nSigma))
                throw new NPPException("Provided buffer is too small.");

            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterUnsharpBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, new NppiPoint(), dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, nRadius, nSigma, nWeight, nThreshold, eBorderType, buffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterUnsharpBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        #endregion

        #region Filter Gauss Advanced

        /// <summary>
        /// Filters the image using a separable Gaussian filter kernel with user supplied floating point coefficients
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="Kernel">Pointer to an array of nFilterTaps kernel coefficients which sum to 1.0F, where nFilterTaps =  2 * ((int)((float)ceil(radius) + 0.5F) ) + 1.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterGauss(NPPImage_16sC3 dst, CudaDeviceVariable<float> Kernel, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.FixedFilters.nppiFilterGaussAdvanced_16s_C3R_Ctx(_devPtrRoi, _pitch, dst.DevicePointerRoi, dst.Pitch, _sizeRoi, Kernel.Size, Kernel.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterGaussAdvanced_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Filters the image using a separable Gaussian filter kernel with user supplied floating point coefficients
        /// </summary>
        /// <param name="dst">Destination-Image</param>
        /// <param name="Kernel">Pointer to an array of nFilterTaps kernel coefficients which sum to 1.0F, where nFilterTaps =  2 * ((int)((float)ceil(radius) + 0.5F) ) + 1.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterGaussBorder(NPPImage_16sC3 dst, CudaDeviceVariable<float> Kernel, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterGaussBorder.nppiFilterGaussAdvancedBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dst.DevicePointerRoi, dst.Pitch, dst.SizeRoi, Kernel.Size, Kernel.DevicePointer, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterGaussAdvancedBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        #endregion

        #region GradientColorToGray


        /// <summary>
        /// 3 channel 16-bit signed packed RGB to 1 channel 16-bit signed packed Gray Gradient conversion.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="eNorm">Gradient distance method to use.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void GradientColorToGray(NPPImage_16sC1 dest, NppiNorm eNorm, NppStreamContext nppStreamCtx)
        {
            NppStatus status = NPPNativeMethods_Ctx.NPPi.GradientColorToGray.nppiGradientColorToGray_16s_C3C1R_Ctx(DevicePointerRoi, Pitch, dest.DevicePointerRoi, dest.Pitch, SizeRoi, eNorm, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiGradientColorToGray_16s_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }

        #endregion

        #region GradientVectorPrewittBorder

        /// <summary>
        /// 3 channel 16-bit signed packed RGB to optional 1 channel 32-bit floating point X (vertical), Y (horizontal), magnitude, 
        /// and/or 32-bit floating point angle gradient vectors with user selectable fixed mask size and distance method with border control.
        /// </summary>
        /// <param name="destX">X vector destination_image_pointer</param>
        /// <param name="destY">Y vector destination_image_pointer.</param>
        /// <param name="destMag">magnitude destination_image_pointer.</param>
        /// <param name="destAngle">angle destination_image_pointer.</param>
        /// <param name="eMaskSize">fixed filter mask size to use.</param>
        /// <param name="eNorm">gradient distance method to use.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void GradientVectorPrewittBorder(NPPImage_32fC1 destX, NPPImage_32fC1 destY, NPPImage_32fC1 destMag, NPPImage_32fC1 destAngle, MaskSize eMaskSize, NppiNorm eNorm, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            NppiSize roi = _sizeRoi;
            int destXPitch = 0;
            CUdeviceptr destXPtr = new CUdeviceptr(0);
            int destYPitch = 0;
            CUdeviceptr destYPtr = new CUdeviceptr(0);
            int destMagPitch = 0;
            CUdeviceptr destMagPtr = new CUdeviceptr(0);
            int destAngPitch = 0;
            CUdeviceptr destAngPtr = new CUdeviceptr(0);

            if (destX != null)
            {
                destXPitch = destX.Pitch;
                destXPtr = destX.DevicePointerRoi;
                roi = destX.SizeRoi;
            }
            if (destY != null)
            {
                destYPitch = destY.Pitch;
                destYPtr = destY.DevicePointerRoi;
                roi = destY.SizeRoi;
            }
            if (destMag != null)
            {
                destMagPitch = destMag.Pitch;
                destMagPtr = destMag.DevicePointerRoi;
                roi = destMag.SizeRoi;
            }
            if (destAngle != null)
            {
                destAngPitch = destAngle.Pitch;
                destAngPtr = destAngle.DevicePointerRoi;
                roi = destAngle.SizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.GradientVectorPrewittBorder.nppiGradientVectorPrewittBorder_16s32f_C3C1R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, destXPtr, destXPitch, destYPtr, destYPitch, destMagPtr, destMagPitch, destAngPtr, destAngPitch, roi, eMaskSize, eNorm, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiGradientVectorPrewittBorder_16s32f_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region GradientVectorScharrBorder

        /// <summary>
        /// 3 channel 16-bit signed packed RGB to optional 1 channel 32-bit floating point X (vertical), Y (horizontal), magnitude, 
        /// and/or 32-bit floating point angle gradient vectors with user selectable fixed mask size and distance method with border control.
        /// </summary>
        /// <param name="destX">X vector destination_image_pointer</param>
        /// <param name="destY">Y vector destination_image_pointer.</param>
        /// <param name="destMag">magnitude destination_image_pointer.</param>
        /// <param name="destAngle">angle destination_image_pointer.</param>
        /// <param name="eMaskSize">fixed filter mask size to use.</param>
        /// <param name="eNorm">gradient distance method to use.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void GradientVectorScharrBorder(NPPImage_32fC1 destX, NPPImage_32fC1 destY, NPPImage_32fC1 destMag, NPPImage_32fC1 destAngle, MaskSize eMaskSize, NppiNorm eNorm, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            NppiSize roi = _sizeRoi;
            int destXPitch = 0;
            CUdeviceptr destXPtr = new CUdeviceptr(0);
            int destYPitch = 0;
            CUdeviceptr destYPtr = new CUdeviceptr(0);
            int destMagPitch = 0;
            CUdeviceptr destMagPtr = new CUdeviceptr(0);
            int destAngPitch = 0;
            CUdeviceptr destAngPtr = new CUdeviceptr(0);

            if (destX != null)
            {
                destXPitch = destX.Pitch;
                destXPtr = destX.DevicePointerRoi;
                roi = destX.SizeRoi;
            }
            if (destY != null)
            {
                destYPitch = destY.Pitch;
                destYPtr = destY.DevicePointerRoi;
                roi = destY.SizeRoi;
            }
            if (destMag != null)
            {
                destMagPitch = destMag.Pitch;
                destMagPtr = destMag.DevicePointerRoi;
                roi = destMag.SizeRoi;
            }
            if (destAngle != null)
            {
                destAngPitch = destAngle.Pitch;
                destAngPtr = destAngle.DevicePointerRoi;
                roi = destAngle.SizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.GradientVectorScharrBorder.nppiGradientVectorScharrBorder_16s32f_C3C1R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, destXPtr, destXPitch, destYPtr, destYPitch, destMagPtr, destMagPitch, destAngPtr, destAngPitch, roi, eMaskSize, eNorm, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiGradientVectorScharrBorder_16s32f_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region GradientVectorSobelBorder

        /// <summary>
        /// 3 channel 16-bit signed packed RGB to optional 1 channel 32-bit floating point X (vertical), Y (horizontal), magnitude, 
        /// and/or 32-bit floating point angle gradient vectors with user selectable fixed mask size and distance method with border control.
        /// </summary>
        /// <param name="destX">X vector destination_image_pointer</param>
        /// <param name="destY">Y vector destination_image_pointer.</param>
        /// <param name="destMag">magnitude destination_image_pointer.</param>
        /// <param name="destAngle">angle destination_image_pointer.</param>
        /// <param name="eMaskSize">fixed filter mask size to use.</param>
        /// <param name="eNorm">gradient distance method to use.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void GradientVectorSobelBorder(NPPImage_32fC1 destX, NPPImage_32fC1 destY, NPPImage_32fC1 destMag, NPPImage_32fC1 destAngle, MaskSize eMaskSize, NppiNorm eNorm, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            NppiSize roi = _sizeRoi;
            int destXPitch = 0;
            CUdeviceptr destXPtr = new CUdeviceptr(0);
            int destYPitch = 0;
            CUdeviceptr destYPtr = new CUdeviceptr(0);
            int destMagPitch = 0;
            CUdeviceptr destMagPtr = new CUdeviceptr(0);
            int destAngPitch = 0;
            CUdeviceptr destAngPtr = new CUdeviceptr(0);

            if (destX != null)
            {
                destXPitch = destX.Pitch;
                destXPtr = destX.DevicePointerRoi;
                roi = destX.SizeRoi;
            }
            if (destY != null)
            {
                destYPitch = destY.Pitch;
                destYPtr = destY.DevicePointerRoi;
                roi = destY.SizeRoi;
            }
            if (destMag != null)
            {
                destMagPitch = destMag.Pitch;
                destMagPtr = destMag.DevicePointerRoi;
                roi = destMag.SizeRoi;
            }
            if (destAngle != null)
            {
                destAngPitch = destAngle.Pitch;
                destAngPtr = destAngle.DevicePointerRoi;
                roi = destAngle.SizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.GradientVectorSobelBorder.nppiGradientVectorSobelBorder_16s32f_C3C1R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, destXPtr, destXPitch, destYPtr, destYPitch, destMagPtr, destMagPitch, destAngPtr, destAngPitch, roi, eMaskSize, eNorm, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiGradientVectorSobelBorder_16s32f_C3C1R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region New in Cuda 9

        /// <summary>
        /// Wiener filter with border control.
        /// </summary>
        /// <param name="dest">destination_image_pointer</param>
        /// <param name="oMaskSize">Pixel Width and Height of the rectangular region of interest surrounding the source pixel.</param>
        /// <param name="oAnchor">Positive X and Y relative offsets of primary pixel in region of interest surrounding the source pixel relative to bottom right of oMaskSize.</param>
        /// <param name="aNoise">Fixed size array of per-channel noise variance level value in range of 0.0F to 1.0F.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterWienerBorder(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, float[] aNoise, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.FilterWienerBorder.nppiFilterWienerBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi, dest.Pitch, dest.SizeRoi, oMaskSize, oAnchor, aNoise, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterWienerBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// 3 channel 16-bit signed color per source image descriptor window location with source image border control 
        /// to per descriptor window destination floating point histogram of gradients. Requires first calling nppiHistogramOfGradientsBorderGetBufferSize function
        /// call to get required scratch (host) working buffer size and nppiHistogramOfGradientsBorderGetDescriptorsSize() function call to get
        /// total size for nLocations of output histogram block descriptor windows.
        /// </summary>
        /// <param name="hpLocations">Host pointer to array of NppiPoint source pixel starting locations of requested descriptor windows. Important: hpLocations is a </param>
        /// <param name="pDstWindowDescriptorBuffer">Output device memory buffer pointer of size hpDescriptorsSize bytes to first of nLoc descriptor windows (see nppiHistogramOfGradientsBorderGetDescriptorsSize() above).</param>
        /// <param name="oHOGConfig">Requested HOG configuration parameters structure.</param>
        /// <param name="pScratchBuffer">Device memory buffer pointer of size hpBufferSize bytes to scratch memory buffer (see nppiHistogramOfGradientsBorderGetBufferSize() above).</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void HistogramOfGradientsBorder(NppiPoint[] hpLocations, CudaDeviceVariable<byte> pDstWindowDescriptorBuffer, NppiHOGConfig oHOGConfig, CudaDeviceVariable<byte> pScratchBuffer, NppiBorderType eBorderType, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.HistogramOfOrientedGradientsBorder.nppiHistogramOfGradientsBorder_16s32f_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, hpLocations, hpLocations.Length, pDstWindowDescriptorBuffer.DevicePointer, _sizeRoi, oHOGConfig, pScratchBuffer.DevicePointer, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiHistogramOfGradientsBorder_16s32f_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }

        /// <summary>
        /// Resizes images.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="eInterpolation">Interpolation mode</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Resize(NPPImage_16sC3 dest, InterpolationMode eInterpolation, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.GeometricTransforms.nppiResize_16s_C3R_Ctx(_devPtr, _pitch, _sizeOriginal, new NppiRect(_pointRoi, _sizeRoi), dest.DevicePointer, dest.Pitch, dest.Size, new NppiRect(dest.PointRoi, dest.SizeRoi), eInterpolation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiResize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }


        /// <summary>
        /// resizes planar images.
        /// </summary>
        /// <param name="src0">Source image (Channel 0)</param>
        /// <param name="src1">Source image (Channel 1)</param>
        /// <param name="src2">Source image (Channel 2)</param>
        /// <param name="dest0">Destination image (Channel 0)</param>
        /// <param name="dest1">Destination image (Channel 1)</param>
        /// <param name="dest2">Destination image (Channel 2)</param>
        /// <param name="eInterpolation">Interpolation mode</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public static void Resize(NPPImage_16sC1 src0, NPPImage_16sC1 src1, NPPImage_16sC1 src2, NPPImage_16sC1 dest0, NPPImage_16sC1 dest1, NPPImage_16sC1 dest2, InterpolationMode eInterpolation, NppStreamContext nppStreamCtx)
        {
            CUdeviceptr[] src = new CUdeviceptr[] { src0.DevicePointer, src1.DevicePointer, src2.DevicePointer };
            CUdeviceptr[] dst = new CUdeviceptr[] { dest0.DevicePointer, dest1.DevicePointer, dest2.DevicePointer };
            NppStatus status = NPPNativeMethods_Ctx.NPPi.GeometricTransforms.nppiResize_16s_P3R_Ctx(src, src0.Pitch, src0.Size, new NppiRect(src0.PointRoi, src0.SizeRoi), dst, dest0.Pitch, dest0.Size, new NppiRect(dest0.PointRoi, dest0.SizeRoi), eInterpolation, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiResize_16s_P3R_Ctx", status));
            NPPException.CheckNppStatus(status, null);
        }
        #endregion
        #region New in Cuda 12.0

#if ADD_MISSING_CTX

        /// <summary>
        /// median filter scratch memory size.
        /// </summary>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public uint FilterMedianBorderGetBufferSize(NppiSize oMaskSize, NppiBorderType eBorderType, NppStreamContext nppStreamCtx)
        {
            uint bufferSize = 0;
            status = NPPNativeMethods_Ctx.NPPi.ImageMedianFilter.nppiFilterMedianBorderGetBufferSize_16s_C3R_Ctx(_sizeRoi, oMaskSize, ref bufferSize, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMedianBorderGetBufferSize_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
            return bufferSize;
        }
        #region Add
        /// <summary>
        /// Add constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value to add</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Add(CudaDeviceVariable<short> nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.AddDeviceConst.nppiAddDeviceC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant.DevicePointer, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAddDeviceC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Add constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value to add</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Add(CudaDeviceVariable<short> nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.AddDeviceConst.nppiAddDeviceC_16s_C3IRSfs_Ctx(nConstant.DevicePointer, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiAddDeviceC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Sub

        /// <summary>
        /// Subtract constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value to subtract</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sub(CudaDeviceVariable<short> nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SubDeviceConst.nppiSubDeviceC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant.DevicePointer, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSubDeviceC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Subtract constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value to subtract</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Sub(CudaDeviceVariable<short> nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.SubDeviceConst.nppiSubDeviceC_16s_C3IRSfs_Ctx(nConstant.DevicePointer, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiSubDeviceC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Mul

        /// <summary>
        /// Multiply constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mul(CudaDeviceVariable<short> nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MulDeviceConst.nppiMulDeviceC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant.DevicePointer, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMulDeviceC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Multiply constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Mul(CudaDeviceVariable<short> nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.MulDeviceConst.nppiMulDeviceC_16s_C3IRSfs_Ctx(nConstant.DevicePointer, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiMulDeviceC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        #region Div

        /// <summary>
        /// Divide constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="dest">Destination image</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(CudaDeviceVariable<short> nConstant, NPPImage_16sC3 dest, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.DivDeviceConst.nppiDivDeviceC_16s_C3RSfs_Ctx(_devPtrRoi, _pitch, nConstant.DevicePointer, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDivDeviceC_16s_C3RSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// Divide constant to image, scale by 2^(-nScaleFactor), then clamp to saturated value. Inplace.
        /// </summary>
        /// <param name="nConstant">Value</param>
        /// <param name="nScaleFactor">scaling factor</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void Div(CudaDeviceVariable<short> nConstant, int nScaleFactor, NppStreamContext nppStreamCtx)
        {
            status = NPPNativeMethods_Ctx.NPPi.DivDeviceConst.nppiDivDeviceC_16s_C3IRSfs_Ctx(nConstant.DevicePointer, _devPtrRoi, _pitch, _sizeRoi, nScaleFactor, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiDivDeviceC_16s_C3IRSfs_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion

        /// <summary>
        /// median filter with border control.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="pBuffer">Pointer to the user-allocated scratch buffer required for the Median operation.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterMedianBorder(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppiBorderType eBorderType, CudaDeviceVariable<byte> pBuffer, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.ImageMedianFilter.nppiFilterMedianBorder_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi,
                                                    dest.Pitch, dest.SizeRoi, oMaskSize, oAnchor, pBuffer.DevicePointer, eBorderType, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterMedianBorder_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }




        /// <summary>
        /// Box filter with border control. 
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="oMaskSize">Width and Height of the neighborhood region for the local Avg operation.</param>
        /// <param name="oAnchor">X and Y offsets of the kernel origin frame of reference w.r.t the source pixel.</param>
        /// <param name="eBorderType">The border type operation to be applied at source image border boundaries.</param>
        /// <param name="pBuffer">Pointer to the user-allocated scratch buffer required for the Median operation.</param>
        /// <param name="filterArea">The area where the filter is allowed to read pixels. The point is relative to the ROI set to source image, the size is the total size starting from the filterArea point. Default value is the set ROI.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FilterBoxBorderAdvanced(NPPImage_16sC3 dest, NppiSize oMaskSize, NppiPoint oAnchor, NppiBorderType eBorderType, CudaDeviceVariable<byte> pBuffer, NppStreamContext nppStreamCtx, NppiRect filterArea = new NppiRect())
        {
            if (filterArea.Size == new NppiSize())
            {
                filterArea.Size = _sizeRoi;
            }
            status = NPPNativeMethods_Ctx.NPPi.LinearFixedFilters2D.nppiFilterBoxBorderAdvanced_16s_C3R_Ctx(_devPtrRoi, _pitch, filterArea.Size, filterArea.Location, dest.DevicePointerRoi,
                                                    dest.Pitch, dest.SizeRoi, oMaskSize, oAnchor, eBorderType, pBuffer.DevicePointer, nppStreamCtx);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nppiFilterBoxBorderAdvanced_16s_C3R_Ctx", status));
            NPPException.CheckNppStatus(status, this);
        }
#endif
        #endregion
        #region New in Cuda 12.1

        /// <summary>
        /// Image fused absdiff and greater than threshold value.
        /// If for a comparison operations absdiff of sourcePixels is greater than pThreshold is true, the output pixel is set
        /// to pValue, otherwise it is set to absdiff of sourcePixels.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FusedAbsDiff_Threshold_GTVal(NPPImage_16sC3 src2, NPPImage_16sC3 dest, short nThreshold, short nValue, NppStreamContext nppStreamCtx)
        {
            unsafe
            {
                IntPtr ptrThreshold = (IntPtr)(&nThreshold);
                IntPtr ptrValue = (IntPtr)(&nValue);

                status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiFusedAbsDiff_Threshold_GTVal_Ctx(_dataType, _nppChannels,
                    _devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, dest.DevicePointerRoi, dest.Pitch, _sizeRoi, ptrThreshold, ptrValue, nppStreamCtx);
            }
            Debug.WriteLine(String.Format("{0:G}, {1} (Datatype: {2}, Channels: {3}): {4}", DateTime.Now, "nppiFusedAbsDiff_Threshold_GTVal_Ctx", _dataType.ToString(), _channels.ToString(), status));
            NPPException.CheckNppStatus(status, this);
        }
        /// <summary>
        /// In place fused absdiff image greater than threshold value.
        /// If for a comparison operations absdiff of sourcePixels is greater than pThreshold is true, the output pixel is set
        /// to pValue, otherwise it is set to absdiff of sourcePixels.
        /// </summary>
        /// <param name="dest">Destination image</param>
        /// <param name="nThreshold">The threshold value.</param>
        /// <param name="nValue">The threshold replacement value.</param>
        /// <param name="nppStreamCtx">NPP stream context.</param>
        public void FusedAbsDiff_Threshold_GTVal(NPPImage_16sC3 src2, short nThreshold, short nValue, NppStreamContext nppStreamCtx)
        {
            unsafe
            {
                IntPtr ptrThreshold = (IntPtr)(&nThreshold);
                IntPtr ptrValue = (IntPtr)(&nValue);

                status = NPPNativeMethods_Ctx.NPPi.Threshold.nppiFusedAbsDiff_Threshold_GTVal_I_Ctx(_dataType, _nppChannels,
                    _devPtrRoi, _pitch, src2.DevicePointerRoi, src2.Pitch, _sizeRoi, ptrThreshold, ptrValue, nppStreamCtx);
            }
            Debug.WriteLine(String.Format("{0:G}, {1} (Datatype: {2}, Channels: {3}): {4}", DateTime.Now, "nppiFusedAbsDiff_Threshold_GTVal_I_Ctx", _dataType.ToString(), _channels.ToString(), status));
            NPPException.CheckNppStatus(status, this);
        }
        #endregion
    }
}
