---
title: "Remove a delivery extension"
description: "Learn how to remove a delivery extension from Reporting Services so the report server doesn't list it as available and deactivates subscriptions that use it."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/06/2017
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "removing delivery extensions"
  - "deleting delivery extensions"
  - "delivery extensions [Reporting Services], removing"
---
# Remove a delivery extension
  To remove a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension, remove the **Extension** element for your delivery extension from the configuration file. After the configuration information is removed, the delivery extension is no longer available to the report server.  
  
 Once a delivery extension's corresponding **Extension** element is removed from the configuration file, it's no longer registered with the report server. The report server removes the entry from the list of delivery extensions and deactivates any subscriptions that use that delivery extension. When a delivery extension is removed, users are no longer able to select it as a method of notification.  
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
