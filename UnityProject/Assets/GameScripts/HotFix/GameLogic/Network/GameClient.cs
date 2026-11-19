using System.Collections.Generic;
using System.Reflection;
using Fantasy;
using Fantasy.Async;
using Fantasy.Helper;
using Fantasy.Network;
using Log = TEngine.Log;

namespace GameLogic
{
    public sealed class GameClient : Singleton<GameClient>
    {
        /// <summary>
        /// Fantasy 网络框架的 Scene 网络由此驱动
        /// </summary>
        public Scene Scene { get; private set; }
        
        public Session  Session { get; private set; }

        public async FTask InitAsync(List<Assembly> assemblies)
        {  
            // ⚠️ 重要: 手动加载程序集必须手动触发 Fantasy 注册
            // RuntimeInitializeOnLoadMethod 只在 Unity 启动时自动执行一次
            // 手动加载的 DLL 不会触发 RuntimeInitializeOnLoadMethod
            // 调用 Assembly.EnsureLoaded() 来触发该程序集中的 Fantasy 框架注册
            if (assemblies != null && assemblies.Count > 0)
            {
                foreach (var assembly in assemblies)
                {
                    assembly.EnsureLoaded(); 
                }
            }

            // 1. 初始化 Fantasy 框架
            await Fantasy.Platform.Unity.Entry.Initialize();
            // 2. 创建一个 Scene（客户端场景）
            Scene = await Scene.Create(SceneRuntimeMode.MainThread);
            Log.Info("Fantasy 初始化完成！");
            
            // 3. 连接到服务器，返回 Session
            Session = Scene.Connect(
                "127.0.0.1:20000", // 服务器地址和端口
                NetworkProtocolType.KCP,        // 网络协议类型
                OnConnectComplete,              // 连接成功回调
                OnConnectFailed,                // 连接失败回调
                OnConnectDisconnect,            // 连接断开回调
                false,                   // 是否 HTTPS 请求（仅用于 WebSocket）
                5000);            // 连接超时时间
            // 消息发送 方式1 
            Session.Send(new C2G_TestMessage()
            {
                Tag = "TestMessage1"
            });
            // 消息发送 方式2 推荐方式
            Session.C2G_TestMessage("TestMessage2");
        }

        protected override void OnRelease()
        {
            Scene?.Dispose();
        }

        private void OnConnectComplete()
        {
            
        }

        private void OnConnectFailed()
        {
            
        }
        
        private void OnConnectDisconnect()
        {
        }
    }
}