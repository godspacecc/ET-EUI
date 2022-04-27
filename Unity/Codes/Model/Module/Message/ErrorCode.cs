namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常
        public const int ERR_NetWorkError = 200002; //网络错误
        public const int ERR_LoginInfoIsNullError = 200003;//登录信息为空错误
        public const int ERR_AccountFormatError = 200004;//账号格式错误
        public const int ERR_PasswordFormatError = 200005;//密码格式错误
        public const int ERR_AccountInfoBlackListError = 200006;//账户在黑名单
        public const int ERR_PasswordError = 200007;//密码错误
        public const int ERR_RequestRepeatedly = 200008;//重复请求登录
        public const int ERR_TokenError = 200009;
        public const int ERR_RoleNameIsNullError = 200010;
        public const int ERR_RoleNameSame = 200011;
        public const int ERR_RoleNoExist = 200012;
        public const int ERR_RequestSceneTypeError = 200013;
        public const int ERR_GateTokenError = 200014;
        public const int ERR_OtherAccountLogin = 200015;
        public const int ERR_SessionPlayerError = 200016;
        public const int ERR_NonePlayerError = 200017;
        public const int ERR_PlayerSessionError = 200018;
        public const int ERR_SessionStateError = 200019;
        public const int ERR_EnterGameError = 200020;
        public const int ERR_ReEnterGameError = 200021;
        public const int ERR_ReEnterGameError2 = 200022;
        public const int ERR_NumericTypeNotExist = 200023;
        public const int ERR_NumericTypeNotAddPoint = 200024;
        public const int ERR_AddPointNotEnough = 200025;
    }
}