---
title: "User Defined Functions and Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [ADOMD.NET]"
  - "ADOMD.NET, user defined functions"
  - "user defined functions [ADOMD.NET]"
  - "ADOMD.NET, UDFs"
  - "ADOMD.NET, stored procedures"
ms.assetid: 07e8aa47-37d4-4bbc-8bff-49e422d12897
author: minewiskan
ms.author: owend
manager: craigg
---
# User Defined Functions and Stored Procedures
  With ADOMD.NET server objects, you can create user defined function (UDF) or stored procedures for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that interact with metadata and data from the server. These in-process methods are called through Multidimensional Expressions (MDX) or Data Mining Extensions (DMX) statements to provide added functionality without the latencies associated with network communications.  
  
## UDF Examples  
 A UDF is a method that can be called in the context of an MDX or DMX statement, can take any number of parameters, and can return any type of data.  
  
 A UDF created using MDX is similar to one created for DMX. The main difference is that certain properties of the <xref:Microsoft.AnalysisServices.AdomdServer.Context> object, such as the <xref:Microsoft.AnalysisServices.AdomdServer.Context.CurrentCube%2A> and <xref:Microsoft.AnalysisServices.AdomdServer.Context.CurrentMiningModel%2A> properties, are available only for one scripting language or the other.  
  
 The following examples show how to use a UDF to return a node description, filter tuples, and apply a filter to a tuple.  
  
### Returning a Node Description  
 The following example creates a UDF that returns the node description for a specified node. The UDF uses the current context in which it is being run, and uses a DMX FROM clause to retrieve the node from the current mining model.  
  
```  
public string GetNodeDescription(string nodeUniqueName)  
{  
   return Context.CurrentMiningModel.GetNodeFromUniqueName(nodeUniqueName).Description;  
}  
```  
  
 Once deployed, the previous UDF example can be called by the following DMX expression, which retrieves the most-likely prediction node. The description contains information that describes the conditions that make up the prediction node.  
  
```  
select Cluster(), SampleAssembly.GetNodeDescription( PredictNodeId(Cluster()) ) FROM [Customer Clusters]  
```  
  
### Returning Tuples  
 The following example takes a set and a return count, and randomly retrieves tuples from the set, returning a final subset:  
  
```  
public Set RandomSample(Set set, int returnCount)  
{  
   //Return the original set if there are fewer tuples  
   //in the set than the number requested.  
   if (set.Tuples.Count <= returnCount)  
      return set;  
  
   System.Random r = new System.Random();  
   SetBuilder returnSet = new SetBuilder();  
  
   //Retrieve random tuples until the return set is filled.  
   int i = set.Tuples.Count;  
   foreach (Tuple t in set.Tuples)  
   {  
      if (r.Next(i) < returnCount)  
      {  
         returnCount--;  
         returnSet.Add(t);  
      }  
      i--;  
      //Stop the loop if we have enough tuples.  
      if (returnCount == 0)  
         break;  
   }  
   return returnSet.ToSet();  
}  
```  
  
 The previous example is called in the following MDX example. In this MDX example, five random states or provinces are retrieved from the **Adventure Works** database.  
  
```  
SELECT SampleAssembly.RandomSample([Geography].[State-Province].Members, 5) on ROWS,   
[Date].[Calendar].[Calendar Year] on COLUMNS  
FROM [Adventure Works]  
WHERE [Measures].[Reseller Freight Cost]  
```  
  
### Applying a Filter to a Tuple  
 In the following example, a UDF is defined that takes a set, and applies a filter to each tuple in the set, using the Expression object. Any tuples that conform to the filter will be added to a set that is returned.  
  
 [!code-csharp[Adomd.NetServer#FilterSet](../../snippets/csharp/SQL14/adomd.net/adomd.netserver/cs/class1.cs#filterset)]  
  
 The previous example is called in the following MDX example, which filters the set to cities with names beginning with 'A'.  
  
```  
Select Measures.Members on Rows,  
SampleAssembly.FilterSet([Customer].[Customer Geography].[City], "[Customer].[Customer Geography].[City].CurrentMember.Name < 'B'") on Columns  
From [Adventure Works]  
```  
  
## Stored Procedure Example  
 In the following example, an MDX-based stored procedure uses AMO to create partitions, if needed, for Internet Sales.  
  
 [!code-csharp[Adomd.NetServer#CreateInternetSalesMeasureGroupPartitions](../../snippets/csharp/SQL14/adomd.net/adomd.netserver/cs/class1.cs#createinternetsalesmeasuregrouppartitions)]  
  
  
