---
title: "Server Properties (Misc Server Settings Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.serverproperties.miscserversettings.f1"
ms.assetid: b170c066-30cd-42dd-8d34-aa129ea09551
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Server Properties (Misc Server Settings Page)
  Use this page to view or modify your server settings.  
  
## Options  
 **Default language for users**  
 Specifies the default language for all newly created logins.  
  
 **Allow triggers to fire other triggers**  
 Controls whether a trigger can perform an action that initiates another trigger. When cleared, triggers cannot be fired by another trigger. When selected, triggers can be fired by other triggers to as many as 32 levels.  
  
 **Use query governor to prevent long-running queries**  
 Specifies an upper limit on the time within which a query can run. Query cost refers to the estimated elapsed time, in seconds, required to execute a query on a specific hardware configuration. By default, the query governor is turned off and all queries are allowed to run. If this option is selected, you must enter a time limit in the text box below. If you specify a nonzero, nonnegative value, the query governor disallows execution of any query that has an estimated cost exceeding that value.  
  
 **Interpret a two-digit year as falling between**  
 Specifies the 100-year date range for interpreting two-digit year values. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will interpret two-digit date values to refer to the year ending in those digits that falls within the specified range.  
  
 Set the right box with the ending year. When the ending year is saved, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will automatically populate the left box with the beginning year.  
  
 **Configured Values**  
 Displays the configured values for the options on this pane. If you change these values, click **Running Values** to see whether the changes have taken effect. If the changes have not taken effect, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be restated first.  
  
 **Running Values**  
 View the currently running values for the options on this pane. These values are read-only.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)  
  
  
