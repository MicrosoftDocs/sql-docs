---
title: Data collection in ReportViewer control
description: ReportViewer collects anonymous usage data to understand how customers use the product and focus development on improvements most relevant to customers.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: application-integration
ms.topic: reference
ms.custom:
  - updatefrequency5
---
# Integrate Reporting Services by using ReportViewer controls - data collection

The control collects anonymous usage data to better understand how customers make use of the product. Usage data enables future development to be focused on improvements that are most relevant to customers.

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
## Related content

- [Use the WebForms ReportViewer control](using-the-webforms-reportviewer-control.md)
- [Integrate Reporting Services by using Report Viewer controls](integrating-reporting-services-using-reportviewer-controls.md)
