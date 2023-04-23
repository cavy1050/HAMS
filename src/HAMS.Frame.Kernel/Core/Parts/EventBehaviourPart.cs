namespace HAMS.Frame.Kernel.Core
{
    public enum EventBehaviourPart
    {
        /// <summary>
        /// 默认值初始化
        /// </summary>
        DefaultInitialization = 1,

        /// <summary>
        /// 自定义值初始化
        /// </summary>
        Initialization,

        /// <summary>
        /// 添加数据
        /// </summary>
        Addition,

        /// <summary>
        /// 修改数据
        /// </summary>
        Alteration,

        /// <summary>
        /// 删除数据
        /// </summary>
        Deletion,

        /// <summary>
        /// 数据激活,生效
        /// </summary>
        Activation,

        /// <summary>
        /// 数据持久化
        /// </summary>
        Persistence
    }
}
