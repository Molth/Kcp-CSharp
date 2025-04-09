using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#if !NETSTANDARD
using System.Runtime.CompilerServices;

#else
using nint = System.IntPtr;
using nuint = System.UIntPtr;
#endif

#pragma warning disable CS1591

// ReSharper disable ALL

namespace kcp
{
    public static unsafe partial class KCP
    {
        public static void* malloc(nuint size)
        {
#if NET6_0_OR_GREATER
            return NativeMemory.Alloc(size);
#elif !NETSTANDARD
            return (void*)Marshal.AllocHGlobal((nint)size);
#else
            return (void*)Marshal.AllocHGlobal((int)size);
#endif
        }

        public static void free(void* memory)
        {
#if NET6_0_OR_GREATER
            NativeMemory.Free(memory);
#else
            Marshal.FreeHGlobal((nint)memory);
#endif
        }

        public static void memcpy(void* dst, void* src, nuint size)
        {
#if !NETSTANDARD
            Unsafe.CopyBlockUnaligned(dst, src, (uint)size);
#else
            Buffer.MemoryCopy(src, dst, (ulong)size, (ulong)size);
#endif
        }

        public static void memset(void* dst, byte val, nuint size)
        {
#if !NETSTANDARD
            Unsafe.InitBlockUnaligned(dst, val, (uint)size);
#else
            for (ulong i = 0; i < (ulong)size; ++i)
                *((byte*)dst) = val;
#endif
        }

        [Conditional("DEBUG")]
        public static void assert(bool condition) => Debug.Assert(condition);

        public static void abort() => Environment.Exit(-1);
    }
}