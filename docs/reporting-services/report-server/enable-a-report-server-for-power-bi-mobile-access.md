---
title: "Enable a report server for Power BI Mobile access"
description: Learn how to connect the Power BI Mobile app to Reporting Services to consume Mobile Reports. Mobile Reports are a feature of Native Mode.
author: maggiesMSFT
ms.author: maggies
ms.date: 12/17/2015
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Enable a report server for Power BI Mobile access
You can use the Power BI Mobile app to consume Mobile Reports. There are a few things that you need to configure to allow the Power BI Mobile app to connect to Reporting Services.  
  
-   [Mobile Reports require Reporting Services native mode](#nativemode)  
-   [Enable basic authentication for Reporting Services](#basicauth) (for CTP 3.2)  
-   [Recommended to enable HTTPS along with a valid certificate trust for the client device](#https)  
-   [Review firewall settings](#firewall)  
  
<a name="nativemode"/> 

## Reporting Services native mode required  
Mobile Reports are a feature of native mode. They aren't available in SharePoint Integrated mode. The Power BI Mobile app only works with a native mode instance.  
  
<a name="basicauth"/>  

## Enable basic authentication  
The iOS Power BI Mobile app requires basic authentication to connect and consume Mobile Reports. Reporting Services isn't configured with basic authentication enabled by default. For information on how to configure basic authentication, see [Configure basic authentication on the report server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md).  
  
You also need to have Windows authentication enabled to allow the publisher app to publish Mobile Report.  
  
<a name="https"/>  

## Enable HTTPS  
You should enable HTTPS in Reporting Services if you enable basic authentication. If you enable HTTPS, make sure that the certificates used can be trusted with the client devices running the iOS Power BI Mobile app. Be sure that the certification chain is valid and available to the client device.  
  
If you need to use a self-signed certificate for development or evaluation purposes, you can export the certificate from the report server and install it on the mobile device. Refer to your device documentation on how to install it on that device.  
  
For more information on enabling TLS, see [Configure TLS connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).

<a name="firewall"/>

## Review firewall settings
You should review your firewall settings to ensure that all devices can connect successfully to Reporting Services.   

For more information on how to configure the Windows Firewall, see [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).  

## Related content

[Configure basic authentication on the report server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md)  
[Configure TLS connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md)  
[Configure a firewall for report server access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)  


  
  
  
  
  
  

