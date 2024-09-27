---
title: "Identify the execution state"
description: Learn to use Reporting Services to identify the execution state so that you can interact with the report in several ways.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "session states [Reporting Services]"
  - "lifetimes [Reporting Services]"
  - "sessions [Reporting Services]"
  - "SessionHeader SOAP header"
---
# Identify the execution state
  Hypertext Transfer Protocol (HTTP) is a connectionless and stateless protocol. Being connectionless and stateless means that it doesn't automatically indicate whether different requests come from the same client or even whether a single browser instance is still actively viewing a page or site. Sessions create a logical connection to maintain state between server and client over HTTP. The user-specific information relevant to a particular session is known as the session state.  
  
 Session management involves correlating an HTTP request with other previous requests generated from the same session. Without session management, these requests appear unrelated to the Report Server Web service because of the connectionless and stateless nature of the HTTP protocol.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't expose a holistic concept of session state such as that exposed by [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]. However, when executing reports, the report server maintains state between method calls in the form of an **execution**. An execution allows the user to interact with the report in several ways - including loading the report from the report server, setting credentials and parameters for the report, and rendering the report.  
  
 While they're communicating to a report server, clients use the execution to manage report viewing and user navigation to other pages in a report, and to show or hide sections of a report. A unique execution exists for each report the client application is running.  
  
 In general, the lifetime of an execution starts when a user navigates to a browser or client application and selects a report to view. The execution is discarded after a short time-out period after the last request to the execution is received (the default time out is 20 minutes).  
  
 From a Web service perspective, the lifetime starts when the Report Server Web service <xref:ReportExecution2005.ReportExecutionService.LoadReport%2A>, <xref:ReportExecution2005.ReportExecutionService.LoadReportDefinition%2A>, or <xref:ReportExecution2005.ReportExecutionService.Render%2A> methods are called. The application can use other methods to manipulate the active execution (for example, setting parameters and setting data sources). The execution is discarded after a short time-out period after the last request to the execution is received (the default time out is 20 minutes).  
  
 An application keeps track of multiple active executions between calls to the Web service <xref:ReportExecution2005.ReportExecutionService.Render%2A> and <xref:ReportExecution2005.ReportExecutionService.RenderStream%2A> methods by saving the <xref:ReportExecution2005.ExecutionHeader.ExecutionID%2A>. The Execution ID is returned in the SOAP header from the <xref:ReportExecution2005.ReportExecutionService.LoadReport%2A> and <xref:ReportExecution2005.ReportExecutionService.LoadReportDefinition%2A> methods.  
  
 The following diagram shows the processing and rendering path for reports.  
  
:::image type="content" source="../../reporting-services/report-server-web-service-net-framework-soap-headers/media/rs-render-process-diagram.gif" alt-text="Diagram that shows the processing and rendering path for reports."::: 
  
 To support the functions described earlier, the current SOAP Render method is split into multiple methods encompassing execution initialization, processing, and rendering phases.  
  
 To programmatically render a report, you must:  
  
-   Load the report or the report definition using <xref:ReportExecution2005.ReportExecutionService.LoadReport%2A> or <xref:ReportExecution2005.ReportExecutionService.LoadReportDefinition%2A>.  
  
-   Check to see if the report needs credentials or parameters by checking the values of the <xref:ReportExecution2005.ExecutionInfo.CredentialsRequired%2A> and <xref:ReportExecution2005.ExecutionInfo.ParametersRequired%2A> properties of the <xref:ReportExecution2005.ExecutionInfo> object returned by <xref:ReportExecution2005.ReportExecutionService.LoadReport%2A> or <xref:ReportExecution2005.ReportExecutionService.LoadReportDefinition%2A>  
  
-   If necessary, set the credentials and/or parameters using the <xref:ReportExecution2005.ReportExecutionService.SetExecutionCredentials%2A> and <xref:ReportExecution2005.ReportExecutionService.SetExecutionParameters%2A> methods.  
  
-   Call the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method to render the report.  
  
 While a report is in session, the underlying report stored in the report server database can change. For example, the report definition can change, the report can be deleted or moved, and user permissions can change. If the report is in an active session, then changes to the underlying report (that is, the report stored in the report server database) don't affect it.  
  
 You can also manage a report session using URL access commands.  
  
## Related content

- [Technical reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)
- [Use Reporting Services SOAP headers](../../reporting-services/report-server-web-service-net-framework-soap-headers/using-reporting-services-soap-headers.md)
