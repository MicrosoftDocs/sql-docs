---
title: "Extending OLAP functionality | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 49a17ff3-94e9-4969-84fc-37d49e63933b
author: minewiskan
ms.author: owend
manager: craigg
---
# Extending OLAP functionality
  As a programmer, you can extend Analysis Services by writing assemblies, personalized extensions, and stored procedures that provide functionality you want to use and repurpose in multiple database applications. Assemblies are used to extend multidimensional models functionality by adding new procedures and functions to the MDX language or by means of the personalization addin.  
  
 Stored procedures can be used to call external routines, simplifying Analysis Services database development and implementation by allowing common code to be developed once and stored in a single location. Stored procedures can be used to add business functionality to your applications that is not provided by the native functionality of MDX.  
  
 Personalizations are custom objects that you add to a cube to provide a behavior that varies by user. Personalizations are not permanent objects in the cube, but are objects that the client application applies dynamically during the user's session. Examples include changing the currency of a monetary value depending on the person accessing the data, providing individualized KPIs, or a targeted suggestion list for regular customers who purchase online.  
  
## In This Section  
 [Extending OLAP through personalizations](extending-olap-through-personalizations.md)  
  
 [Analysis Services Personalization Extensions](analysis-services-personalization-extensions.md)  
  
 [Defining Stored Procedures](../../multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
