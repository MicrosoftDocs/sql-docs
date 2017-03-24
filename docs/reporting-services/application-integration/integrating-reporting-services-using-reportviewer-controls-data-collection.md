---
title: "Data collection in ReportViewer Control 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "09/06/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 112e0240-351d-46a9-98c7-2be09f26ac60
caps.latest.revision: 2
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Integrating Reporting Services Using ReportViewer Controls - Data Collection
By default, the ReportViewer Control collects anonymous usage information in order for Microsoft to better understand how customers are making use of the control. By creating a better understanding of how customers are deploying and using the Viewer Control, future development can be focused on improvements that deliver the most value to customers.

For an explanation of the user data collection and use practices for Microsoft SQL Server 2016 releases and any other products and services, please refer to this [privacy statement from Microsoft](https://www.microsoft.com/EN-US/privacystatement/SQLServer/Default.aspx).

## Opting out of telemetry

Telemetry can be disabled programmatically through the “EnableTelemetry”. This can be done by editing the .aspx page hosting the control

```
\<rsweb:ReportViewer ID="ReportViewer1" runat="server" EnableTelemetry="false">
\</rsweb:ReportViewer>
```

Or, pragmatically before the control is rendered such as in the hosting page’s Page_Load call.
    
```
protected void Page_Load(object sender, EventArgs e)
{
    ReportViewer1.EnableTelemetry = false;
}
```
## See also

[Using the WebForms ReportViewer Control](../../reporting-services/application-integration/using-the-webforms-reportviewer-control.md)  
[Integrating Reporting Services Using the ReportViewer Controls](../../reporting-services/application-integration/integrating-reporting-services-using-reportviewer-controls.md) 



