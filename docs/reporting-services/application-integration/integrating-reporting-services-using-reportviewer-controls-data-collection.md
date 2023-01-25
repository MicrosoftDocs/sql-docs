---
title: Data collection in ReportViewer Control
description: ReportViewer collects anonymous usage data to understand how customers use the product and focus development on improvements most relevant to customers.
author: maggiesMSFT
ms.author: maggies
ms.custom: seo-lt-2019
ms.reviewer: ""
ms.service: reporting-services
ms.subservice: application-integration
ms.topic: reference
ms.date: 06/03/2020
---
# Integrate Reporting Services Using ReportViewer Controls - Data Collection

Anonymous usage data is collected by the control to better understand how customers make use of the product. Usage data enables future development to be focused on improvements that are most relevant to customers.

An explanation of the data collection and usage practices of Microsoft SQL Server and Report Viewer are available in the [privacy statement](../../sql-server/sql-server-privacy.md).

## Opting out of data collection

Collection of usage data can be disabled through the ```EnableTelemetry``` property.

```xml
<rsweb:ReportViewer ID="ReportViewer1" runat="server" EnableTelemetry="false">
</rsweb:ReportViewer>
```

Or pragmatically before the control is rendered.
    
```csharp
protected void Page_Load(object sender, EventArgs e)
{
    ReportViewer1.EnableTelemetry = false;
}
```
## See also

[Using the WebForms Report Viewer Control](../../reporting-services/application-integration/using-the-webforms-reportviewer-control.md)  
[Integrating Reporting Services Using the Report Viewer Controls](../../reporting-services/application-integration/integrating-reporting-services-using-reportviewer-controls.md)