using System.Collections.Generic;
using System.Reflection;
using Fantasy;
using Fantasy.Async;
using Fantasy.Helper;
using Log = TEngine.Log;

namespace GameLogic
{
    public sealed class GameClient : Singleton<GameClient>
    {
        /// <summary>
        /// Fantasy 网络框架的 Scene 网络由此驱动
        /// </summary>
        public Scene Scene { get; private set; }

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
        }

        protected override void OnRelease()
        {
            Scene?.Dispose();
        }
    }
}