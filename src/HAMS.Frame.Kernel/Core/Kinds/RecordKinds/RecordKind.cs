namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 操作信息类型 记录程序框架操作信息
    /// </summary>
    public class RecordKind : BaseKind
    {
        /// <summary>
        /// 记录时间 时间格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public virtual string RecordTime { get; set; }
    }
}
