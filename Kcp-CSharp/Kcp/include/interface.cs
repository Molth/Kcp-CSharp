#pragma warning disable CS1591
#pragma warning disable CS8632

// ReSharper disable ALL

namespace kcp
{
    //---------------------------------------------------------------------
    // interface
    //---------------------------------------------------------------------
    public static unsafe class KCP_INTERFACE
    {
        // create a new kcp control object, 'conv' must equal in two endpoint
        // from the same connection. 'user' will be passed to the output callback
        // output callback can be setup like this: 'kcp->output = my_udp_output'
        public static IKCPCB* ikcp_create(uint conv, void* user) => KCP.ikcp_create(conv, user);

        // release kcp control object
        public static void ikcp_release(IKCPCB* kcp) => KCP.ikcp_release(kcp);

        // set output callback, which will be invoked by kcp
        public static void ikcp_setoutput(IKCPCB* kcp, delegate* managed<byte*, int, IKCPCB*, void*, int> output) => KCP.ikcp_setoutput(kcp, output);

        // user/upper level recv: returns size, returns below zero for EAGAIN
        public static int ikcp_recv(IKCPCB* kcp, byte* buffer, int len) => KCP.ikcp_recv(kcp, buffer, len);

        // user/upper level send, returns below zero for error
        public static int ikcp_send(IKCPCB* kcp, byte* buffer, int len) => KCP.ikcp_send(kcp, buffer, len);

        // update state (call it repeatedly, every 10ms-100ms), or you can ask 
        // ikcp_check when to call it again (without ikcp_input/_send calling).
        // 'current' - current timestamp in millisec. 
        public static void ikcp_update(IKCPCB* kcp, uint current) => KCP.ikcp_update(kcp, current);

        // Determine when should you invoke ikcp_update:
        // returns when you should invoke ikcp_update in millisec, if there 
        // is no ikcp_input/_send calling. you can call ikcp_update in that
        // time, instead of call update repeatly.
        // Important to reduce unnacessary ikcp_update invoking. use it to 
        // schedule ikcp_update (eg. implementing an epoll-like mechanism, 
        // or optimize ikcp_update when handling massive kcp connections)
        public static uint ikcp_check(IKCPCB* kcp, uint current) => KCP.ikcp_check(kcp, current);

        // when you received a low level packet (eg. UDP packet), call it
        public static int ikcp_input(IKCPCB* kcp, byte* data, long size) => KCP.ikcp_input(kcp, data, size);

        // flush pending data
        public static void ikcp_flush(IKCPCB* kcp) => KCP.ikcp_flush(kcp);

        // check the size of next message in the recv queue
        public static int ikcp_peeksize(IKCPCB* kcp) => KCP.ikcp_peeksize(kcp);

        // change MTU size, default is 1400
        public static int ikcp_setmtu(IKCPCB* kcp, int mtu) => KCP.ikcp_setmtu(kcp, mtu);

        // set maximum window size: sndwnd=32, rcvwnd=32 by default
        public static int ikcp_wndsize(IKCPCB* kcp, int sndwnd, int rcvwnd) => KCP.ikcp_wndsize(kcp, sndwnd, rcvwnd);

        // get how many packet is waiting to be sent
        public static int ikcp_waitsnd(IKCPCB* kcp) => KCP.ikcp_waitsnd(kcp);

        // fastest: ikcp_nodelay(kcp, 1, 20, 2, 1)
        // nodelay: 0:disable(default), 1:enable
        // interval: internal update timer interval in millisec, default is 100ms 
        // resend: 0:disable fast resend(default), 1:enable fast resend
        // nc: 0:normal congestion control(default), 1:disable congestion control
        public static int ikcp_nodelay(IKCPCB* kcp, int nodelay, int interval, int resend, int nc) => KCP.ikcp_nodelay(kcp, nodelay, interval, resend, nc);

        public static void ikcp_log(IKCPCB* kcp, int mask, string fmt, params object?[] args) => KCP.ikcp_log(kcp, mask, fmt, args);

        // setup allocator
        public static void ikcp_allocator(delegate* managed<nuint, void*> new_malloc, delegate* managed<void*, void> new_free) => KCP.ikcp_allocator(new_malloc, new_free);

        // read conv
        public static uint ikcp_getconv(void* ptr) => KCP.ikcp_getconv(ptr);
    }
}