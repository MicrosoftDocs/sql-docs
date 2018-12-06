---
title: "Key Performance Indicators (KPIs) in Multidimensional Models | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Key Performance Indicators (KPIs) in Multidimensional Models
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In business terminology, a Key Performance Indicator (KPI) is a quantifiable measurement for gauging business success.  
  
 In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a KPI is a collection of calculations that are associated with a measure group in a cube that are used to evaluate business success. Typically, these calculations are a combination of Multidimensional Expressions (MDX) expressions or calculated members. KPIs also have additional metadata that provides information about how client applications should display the results of the KPI's calculations.  
  
 A KPI handles information about a goal set, the actual formula of the performance recorded in the cube, and measurement to show the trend and the status of the performance. AMO is used to define the formulas and other definitions about the values of a KPI. A query interface, such as ADOMD.NET, is used by the client application to retrieve and expose the KPI values to the end user. For more information see [Developing with ADOMD.NET](https://docs.microsoft.com/bi-reference/adomd/developing-with-adomd-net).  
  
 A simple <xref:Microsoft.AnalysisServices.Kpi> object is composed of: basic information, the goal, the actual value achieved, a status value, a trend value, and a folder where the KPI is viewed. Basic information includes the name and description of the KPI. The goal is an MDX expression that evaluates to a number. The actual value is an MDX expression that evaluates to a number. The status and trend value are MDX expressions that evaluate to a number. The folder is a suggested location for the KPI to be presented to the client.  
  
 In business terminology, a Key Performance Indicator (KPI) is a quantifiable measurement for gauging business success. A KPI is frequently evaluated over time. For example, the sales department of an organization may use monthly gross profit as a KPI, but the human resources department of the same organization may use quarterly employee turnover. Each is an example of a KPI. Business executives frequently consume KPIs that are grouped together in a business scorecard to obtain a quick and accurate historical summary of business success.  
  
 In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a KPI is a collection of calculations, which are associated with a measure group in a cube, that are used to evaluate business success. Typically, these calculations are a combination of Multidimensional Expressions (MDX) expressions and calculated members. KPIs also have additional metadata that provides information about how client applications should display the results of a KPI's calculation.  
  
 One key advantage of KPIs in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is that they are server-based KPIs that are consumable by different client applications. A server-based KPI presents a single version of the truth, compared to separate versions of the truth from separate client applications. Moreover, performing the sometimes complex calculations on the server instead of on each client computer may have performance benefits.  
  
## Common KPI Terms  
 The following table provides definitions for common KPI terms in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
|Term|Definition|  
|----------|----------------|  
|Goal|An MDX numeric expression or a calculation that returns the target value of the KPI.|  
|Value|An MDX numeric expression that returns the actual value of the KPI.|  
|Status|An MDX expression that represents the state of the KPI at a specified point in time.<br /><br /> The status MDX expression should return a normalized value between -1 and 1. Values equal to or less than -1 will be interpreted as "bad" or "low." A value of zero (0) is interpreted as "acceptable" or "medium." Values equal to or greater than 1 will be interpreted as "good" or "high."<br /><br /> An unlimited number of intermediate values can optionally be returned and can be used to display any number of additional states, if supported by the client application.|  
|Trend|An MDX expression that evaluates the value of the KPI over time. The trend can be any time-based criterion that is useful in a specific business context.<br /><br /> The trend MDX expression enables a business user to determine whether the KPI is improving over time or degrading over time.|  
|Status indicator|A visual element that provides a quick indication of the status for a KPI. The display of the element is determined by the value of the MDX expression that evaluates status.|  
|Trend indicator|A visual element that provides a quick indication of the trend for a KPI. The display of the element is determined by the value of the MDX expression that evaluates trend.|  
|Display folder|The folder in which the KPI will appear when a user is browsing the cube.|  
|Parent KPI|A reference to an existing KPI that uses the value of the child KPI as part of computation of the parent KPI. Sometimes, a single KPI will be a computation that consists of the values for other KPIs. This property facilitates the correct display of the child KPIs underneath the parent KPI in client applications.|  
|Current time member|An MDX expression that returns the member that identifies the temporal context of the KPI.|  
|Weight|An MDX numeric expression that assigns a relative importance to a KPI. If the KPI is assigned to a parent KPI, the weight is used to proportionally adjust the results of the child KPI value when calculating the value of the parent KPI.|  
  
## Parent KPIs  
 An organization may track different business metrics at different levels. For example, only two or three KPIs may be used to gauge business success for the whole company, but these company-wide KPIs may be based on three or four other KPIs tracked by the business units throughout the company. Also, business units in a company may use different statistics to calculate the same KPI, the results of which are rolled up to the company-wide KPI.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] lets you define a parent-child relationship between KPIs. This parent-child relationship lets the results of the child KPI be used to calculate the results of the parent KPI. Client applications can also use this relationship to appropriately display parent and child KPIs.  
  
## Weights  
 Weights can also be assigned to child KPIs. Weights enable [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to proportionally adjust the results of the child KPI when calculating the value of the parent KPI.  
  
## Retrieving and Displaying KPIs  
 The display of KPIs depends on the implementation of the client application. For example, selecting **Browser View** on the toolbar on the **KPIs** tab of Cube Designer demonstrates one possible client implementation, with graphics used to display status and trend indicators, display folders used to group KPIs, and child KPIs displayed under parent KPIs.  
  
 You can use MDX functions to retrieve individual sections of the KPI, such as the value or goal, for use in MDX expressions, statements, and scripts.  
  
  
