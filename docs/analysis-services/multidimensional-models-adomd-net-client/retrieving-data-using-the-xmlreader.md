---
title: "Retrieving Data Using the XmlReader | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: adomd
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Retrieving Data Using the XmlReader
  The **XmlReader** class, part of the **System.Xml** namespace for the Microsoft .NET Framework Class Library, is similar to the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> class in that the **XmlReader** class also provides fast, non-cached, forward-only access to data. If there is no need for an in-memory, analytical view of the data using the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object, the **XmlReader** object is perfect for retrieving XML data, especially for large quantities of data. Because **XmlReader** streams data, **XmlReader** does not have to retrieve and cache all the data before exposing the data to the caller, as would be the case if a <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object were used to convert the XML for Analysis response into an analytical object model representation.  
  
 The **XmlReader** class provides direct access to the XML for Analysis response received by ADOMD.NET when the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteXmlReader%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object is called. Because the retrieved data is raw XML, you must parse the data and metadata manually. As soon as the data has been retrieved, the **XmlReader** object should be closed.  
  
## Retrieving Data and Metadata  
 To use the **XmlReader** class to retrieve data, you follow these steps:  
  
1.  **Create a new instance of the object.**  
  
     To create a new instance of the **XmlReader** class, you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.Execute%2A> or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteXmlReader%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object.  
  
2.  **Retrieve data.**  
  
     After the command runs the query and returns an **XmlReader**, you must parse the data and metadata. The XML data and metadata is presented in the native format used by the XML for Analysis provider. For most XML for Analysis providers, the native format is the **MDDataSet** format. The **MDDataSet** format provides both data and metadata for cellsets in a well-structured format. For more information about the **MDDataSet** format, see the XML for Analysis specification.  
  
3.  **Close the reader.**  
  
     You should always call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Close%2A> method when you have finished using the **XmlReader** object. While an **XmlReader** is open, that **XmlReader** has exclusive use of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object that was used to run the command. You will not be able to run any commands using that <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection>, including creating another **XmlReader** or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>, until you close the original **XmlReader**.  
  
### Example of Retrieving Data from the XmlReader  
 The following example runs a command and retrieves the data as an **XmlReader**, outputting the contents of the file to the console.  
  
 [!code-cs[Adomd.NetClient#OutputDataWithXML](../../analysis-services/multidimensional-models-adomd-net-client/codesnippet/csharp/retrieving-data-using-th_1_1.cs)]  
  
## See Also  
 [Retrieving Data from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-from-an-analytical-data-source.md)   
 [Retrieving Data Using the CellSet](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-cellset.md)   
 [Retrieving Data Using the AdomdDataReader](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-adomddatareader.md)  
  
  
