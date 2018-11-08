---
title: "Enable a report server for Power BI Mobile access | Microsoft Docs"
ms.date: 12/17/2015
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: report-server

ms.topic: conceptual
ms.assetid: c1a71522-394b-46a7-b9ec-f964bdd81d82
author: markingmyname
ms.author: maghan
---
# Enable a report server for Power BI Mobile access
You can use the Power BI Mobile app to consume Mobile Reports. There are a few things that you need to configure to allow the Power BI Mobile app to connect to Reporting Services.  
  
-   [Mobile Reports require Reporting Services Native Mode](#nativemode)  
-   [Enable basic authentication for Reporting Services](#basicauth) (for CTP 3.2)  
-   [Recommended to enable HTTPS along with a valid certificate trust for the client device](#https)  
-   [Review firewall settings](#firewall)  
  
<a name="nativemode"/>  
## Reporting Services Native Mode required  
Mobile Reports are a feature of Native Mode. They are not available in SharePoint Integrated mode. The Power BI Mobile app will only work with a Native Mode instance.  
  
<a name="basicauth"/>  
## Enable basic authentication  
The iOS Power BI Mobile app requires basic authentication in order to connect and consume Mobile Reports. Reporting Services is not configured with basic authentication enabled by default. For information on how to configure basic authentication, see [Configure Basic Authentication on the Report Server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md).  
  
You will also need to have Windows authentication enabled to allow the publisher app to publish Mobile Report.  
  
<a name="https"/>  
## Enable HTTPS  
It is recommended that you enable HTTPS in Reporting Services if you enable basic authentication. If you enable HTTPS, make sure that the certificates used can be trusted with the client devices running the iOS Power BI Mobile app. This means that the certification chain needs to be valid and available to the client device.  
  
If you need to use a self-signed certificate for development or evaluation purposes, you can export the certificate from the report server and install it on the mobile device. Please refer to your device documentation on how to install it on that device.  
  
For more information on enabling SSL, see [Configure SSL Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).  
  
<a name="firewall"/>  
## Review firewall settings  
It is recommended that you review your firewall settings to ensure that all devices can connect successfully to Reporting Services.   
  
For more information on how to configure the Windows Firewall, see [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).  
  
## See also  
  
[Configure Basic Authentication on the Report Server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md)  
[Configure SSL Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md)  
[Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)  
  
  
  
  
  
  

