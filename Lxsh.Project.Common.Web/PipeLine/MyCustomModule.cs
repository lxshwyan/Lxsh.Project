using System;
using System.Web;

namespace Lxsh.Project.Common.Web.PipeLine
{
    public class MyCustomModule : IHttpModule
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //此处放置清除代码。
        }

        public void Init(HttpApplication application)
        {
            application.AcquireRequestState += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "AcquireRequestState        "));
            application.AuthenticateRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "AuthenticateRequest        "));
            application.AuthorizeRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "AuthorizeRequest           "));
            application.BeginRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "BeginRequest               "));
            application.Disposed += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "Disposed                   "));
            application.EndRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "EndRequest                 "));
            application.Error += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "Error                      "));
            application.LogRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "LogRequest                 "));
            application.MapRequestHandler += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "MapRequestHandler          "));
            application.PostAcquireRequestState += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostAcquireRequestState    "));
            application.PostAuthenticateRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostAuthenticateRequest    "));
            application.PostAuthorizeRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostAuthorizeRequest       "));
            application.PostLogRequest += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostLogRequest             "));
            application.PostMapRequestHandler += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostMapRequestHandler      "));
            application.PostReleaseRequestState += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostReleaseRequestState    "));
            application.PostRequestHandlerExecute += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostRequestHandlerExecute  "));
            application.PostResolveRequestCache += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostResolveRequestCache    "));
            application.PostUpdateRequestCache += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PostUpdateRequestCache     "));
            application.PreRequestHandlerExecute += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PreRequestHandlerExecute   "));
            application.PreSendRequestContent += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PreSendRequestContent      "));
            application.PreSendRequestHeaders += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "PreSendRequestHeaders      "));
            application.ReleaseRequestState += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "ReleaseRequestState        "));
            application.RequestCompleted += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "RequestCompleted           "));
            application.ResolveRequestCache += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "ResolveRequestCache        "));
            application.UpdateRequestCache += (s, e) => application.Response.Write(string.Format("<h1 style='color:#00f'>来自MyCustomModule 的处理，{0}请求到达 {1}</h1><hr>", DateTime.Now.ToString(), "UpdateRequestCache         "));


            //context.AuthenticateRequest	//验证请求，一般用来取得请求用户的信息
            //context.PostAuthenticateRequest	已经获取请求用户的信息
            //context.AuthorizeRequest	授权，一般用来检查用户的请求是否获得权限
            //context.PostAuthorizeRequest	用户请求已经得到授权
            //context.ResolveRequestCache	获取以前处理缓存的处理结果，如果以前缓存过，那么，不必再进行请求的处理工作，直接返回缓存结果
            //context.PostResolveRequestCache	已经完成缓存的获取操作
            //context.PostMapRequestHandler	已经根据用户的请求，创建了处理请求的处理器对象
            //context.AcquireRequestState	取得请求的状态，一般用于Session
            //context.PostAcquireRequestState	已经取得了Session
            //context.PreRequestHandlerExecute	准备执行处理程序
            //context.PostRequestHandlerExecute	已经执行了处理程序
            //context.ReleaseRequestState	释放请求的状态
            //context.PostReleaseRequestState	已经释放了请求的状态
            //context.UpdateRequestCache	更新缓存
            //context.PostUpdateRequestCache	已经更新了缓存
            //context.LogRequest	请求的日志操作
            //context.PostLogRequest	已经完成了请求的日志操作

        }

        #endregion
    }
}
