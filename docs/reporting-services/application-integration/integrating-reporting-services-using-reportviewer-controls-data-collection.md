---
title: Data collection in ReportViewer Control 2016
uthor: markingmyname
ms.author: maghan
manager: kfile
ms.reviewer: ""
ms.prod: reporting-services
ms.prod_service: reporting-services-native, reporting-services-sharepoint
ms.technology: application-integration
ms.topic: reference
ms.custom: ""
ms.date: 09/18/2018
---

# Integrating Reporting Services Using ReportViewer Controls - Data Collection

Anonymous usage data is collected by the control to better understand how customers make use of the product. Usage data enables future development to be focused on improvements that are most relevant to customers.

An explanation of the data collection and usage practices of Microsoft SQL Server and Report Viewer are available in the [privacy statement](https://go.microsoft.com/fwlink/?LinkID=868444).

## Opting out of data collection

Collection of usage data can be disabled through the ```EnableTelemetry``` property.

```
<rsweb:ReportViewer ID="ReportViewer1" runat="server" EnableTelemetry="false">
</rsweb:ReportViewer>
```

Or pragmatically before the control is rendered.
    
```
protected void Page_Load(object sender, EventArgs e)
{
    ReportViewer1.EnableTelemetry = false;
}
```
## See also

[Using the WebForms Report Viewer Control](../../reporting-services/application-integration/using-the-webforms-reportviewer-control.md)  
[Integrating Reporting Services Using the Report Viewer Controls](../../reporting-services/application-integration/integrating-reporting-services-using-reportviewer-controls.md) 



