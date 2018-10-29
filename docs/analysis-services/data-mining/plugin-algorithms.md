---
title: "Plugin Algorithms | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Plugin Algorithms
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In addition to the algorithms that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides, there are many other algorithms that you can use for data mining. Accordingly, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides a mechanism for "plugging in" algorithms that are created by third parties. As long as the algorithms follow certain standards, you can use them within [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] just as you use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms. Plugin algorithms have all the capabilities of algorithms that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides.  
  
 For a full description of the interfaces that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses to communicate with plugin algorithms, see the samples for creating a custom algorithm and custom model viewer that are published on [CodePlex](http://go.microsoft.com/fwlink/?LinkID=87843) Web site.  
  
## Algorithm Requirements  
 To plug an algorithm into [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you must implement the following COM interfaces:  
  
 **IDMAlgorithm**  
 Implements an algorithm that produces models, and implements the prediction operations of the resulting models.  
  
 **IDMAlgorithmNavigation**  
 Enables browsers to access the content of the models.  
  
 **IDMPersist**  
 Enables the models that the algorithm trains to be saved and loaded by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 **IDMAlgorithmMetadata**  
 Describes the capabilities and input parameters of the algorithm.  
  
 **IDMAlgorithmFactory**  
 Creates instances of the objects that implement the algorithm interface, and provides [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] with access to the algorithm-metadata interface.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses these COM interfaces to communicate with plugin algorithms. Although plugin algorithms that you use must support the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB for Data Mining specification, they do not have to support all the data mining options in the specification. You can use the [MINING_SERVICES](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-services-rowset) schema rowset to determine the capabilities of an algorithm. This schema rowset lists the data mining support options for each plugin algorithm provider.  
  
 You must register new algorithms before you use them with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. To register an algorithm, include the following information in the .ini file of the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on which you want to include the algorithms:  
  
-   The algorithm name  
  
-   ProgID (this is optional and will only be included for plugin algorithms)  
  
-   A flag that indicates whether the algorithm is enabled or not  
  
 The following code sample illustrates how to register a new algorithm:  
  
 `<ConfigurationSettings>`  
  
 `...`  
  
 `<DataMining>`  
  
 `...`  
  
 `<Algorithms>`  
  
 `...`  
  
 `<Sample_Plugin_Algorithm>`  
  
 `<Enabled>1</Enabled>`  
  
 `<ProgID>Microsoft.DataMining.SamplePlugInAlgorithm.Factory</ProgID>`  
  
 `</Sample_PlugIn_Algorithm>`  
  
 `...`  
  
 `</Algorithms>`  
  
 `...`  
  
 `</DataMining>`  
  
 `...`  
  
 `</ConfigurationSettings>`  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [DMSCHEMA_MINING_SERVICES Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-services-rowset)  
  
  
