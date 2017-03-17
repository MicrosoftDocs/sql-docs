---
title: "DMSCHEMA_MINING_MODEL_CONTENT Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DMSCHEMA_MINING_MODEL_CONTENT"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_MODEL_CONTENT rowset"
ms.assetid: 1e85d9e7-3b74-42ac-b94e-f52f76d8a25d
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_MODEL_CONTENT Rowset
  Allows the client application to browse the content of a data mining model. Client applications can use the special tree operation restrictions described at the end of this topic to navigate the content of the mining model.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_MODEL_CONTENT** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**||The catalog name. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] populates this column with the name of the database of which the model is a member.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **VT_NULL**.|  
|**MODEL_NAME**|**DBTYPE_WSTR**||The name of the model with which the content described by this row is associated.|  
|**ATTRIBUTE_NAME**|**DBTYPE_WSTR**||The names of the attributes that correspond to this node.|  
|**NODE_NAME**|**DBTYPE_WSTR**||The name of the node. Currently, this column contains the same value as **NODE_UNIQUE_NAME**, though this may change in future releases.|  
|**NODE_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the node.|  
|**NODE_TYPE**|**DBTYPE_I4**||The type of the node. Will generate one of the following values (3rd party data mining algorithms can extend this list):<br /><br /> **DM_NODE_TYPE_CLASSIFICATION_TREE_ROOT** (**2**)<br /><br /> **DM_NODE_TYPE_TREE_INTERIOR** (**3**)<br /><br /> **DM_NODE_TYPE_TREE_DISTRIBUTION** (**4**)<br /><br /> **DM_NODE_TYPE_CLUSTER** (**5**)<br /><br /> **DM_NODE_TYPE_UNKNOWN** (**6**)<br /><br /> **DM_NODE_TYPE_ITEMSET** (**7**)<br /><br /> **DM_NODE_TYPE_ASSOCIATION_RULE** (**8**)<br /><br /> **DM_NODE_TYPE_NB_PREDICTABLE_ATTRIBUTE** (**9**)<br /><br /> **DM_NODE_TYPE_NB_INPUT_ATTRIBUTE** (**10**)<br /><br /> **DM_NODE_TYPE_NB_INPUT_ATTRIBUTE_STATE** (**11**)<br /><br /> **DM_NODE_TYPE_SEQUENCE** (**13**)<br /><br /> **DM_NODE_TYPE_TRANSITION** (**14**)<br /><br /> **DM_NODE_TYPE_TIME_SERIES** (**15**)<br /><br /> **DM_NODE_TYPE_TS_TREE** (**16**)<br /><br /> **DM_NODE_TYPE_NN_SUBNETWORK** (**17**)  Neural network, subnetwork<br /><br /> **DM_NODE_TYPE_NN_INPUT_LAYER** (**18**)  Neural network, input layer (parent of input nodes)<br /><br /> **DM_NODE_TYPE_NN_HIDDEN_LAYER** (**19**) Neural network, hidden layer (parent of hidden nodes)<br /><br /> **DM_NODE_TYPE_NN_OUTPUT_LAYER** (**20**) Neural network, output layer (parent of output nodes)<br /><br /> **DM_NODE_TYPE_NN_INPUT_NODE** (**21**) Neural network, input node<br /><br /> **DM_NODE_TYPE_NN_HIDDEN_NODE** (**22**) Neural network, hidden node<br /><br /> **DM_NODE_TYPE_NN_OUTPUT_NODE** (**23**) Neural network, output node<br /><br /> **DM_NODE_TYPE_NN_MARGINAL_STAT_NODE** (**24**) Neural network, marginal stat node<br /><br /> **DM_NODE_TYPE_REGRESSION_TREE_ROOT** (**25**)<br /><br /> **DM_NODE_TYPE_NB_MARGINAL_STAT_NODE** (**26**) Neural network, marginal stat node|  
|**NODE_GUID**|**DBTYPE_GUID**||The node GUID. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**NODE_CAPTION**|**DBTYPE_WSTR**||A label or a caption associated with the node. This property is primarily for display purposes.|  
|**CHILDREN_CARDINALITY**|**DBTYPE_UI4**||An estimate of the number of children that the node has.|  
|**PARENT_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the node's parent. **NULL** is returned for any nodes at the root level.|  
|**NODE_DESCRIPTION**|**DBTYPE_WSTR**||A user-friendly description of the node.|  
|**NODE_RULE**|**DBTYPE_WSTR**||An XML description of the rule that is embedded in the node.|  
|**MARGINAL_RULE**|**DBTYPE_WSTR**||An XML description of the rule that is moving to the node from the parent node.|  
|**NODE_PROBABILITY**|**DBTYPE_R8**||The probability associated with this node.|  
|**MARGINAL_PROBABILITY**|**DBTYPE_R8**||The probability of reaching the node from the parent node.|  
|**NODE_DISTRIBUTION**|**DBTYPE_HCHAPTER**||A table that contains the probability histogram of the node.|  
|**NODE_SUPPORT**|**DBTYPE_R8**||The number of cases that support this node.|  
|**MSOLAP_MODEL_COLUMN**|**DBTYPE_WSTR**||The name of the column from the model definition to which this node pertains.|  
|**MSOLAP_NODE_SCORE**|**DBTYPE_R8**||The score that was computed for this node.|  
|**MSOLAP_NODE_SHORT_CAPTION**|**DBTYPE_WSTR**||A short caption for the node that can be used for display purposes to improve readability.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_MODEL_CONTENT** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_NAME**|**DBTYPE_WSTR**|Optional.|  
|**ATTRIBUTE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**NODE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**NODE_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**NODE_TYPE**|**DBTYPE_I4**|Optional.|  
|**NODE_GUID**|**DBTYPE_WSTR**|Optional.|  
|**NODE_CAPTION**|**DBTYPE_WSTR**|Optional.|  
|**TREE_OPERATION**|**DBTYPE_UI4**|Optional. See below for additional remarks.|  
  
 The restriction, **TREE_OPERATION**, is not on any particular column of the **DMSCHEMA_MINING_MODEL_CONTENT** rowset; rather, it specifies a tree operator. The consumer can specify a **NODE_UNIQUE_NAME** restriction and the tree operator (**ANCESTORS**, **CHILDREN**, **SIBLINGS**, **PARENT**, **DESCENDANTS**, **SELF**) to obtain the requested set of members. The **SELF** operator includes the row for the node itself in the list of returned rows. The following table describes the constants that make up the bitmap definition for the **TREE_OPERATION** restriction. They can be combined using the logical **OR** operator.  
  
|Constant|Value|  
|--------------|-----------|  
|**DMTREEOP_ANCESTORS**|**0x00000020**|  
|**DMTREEOP_CHILDREN**|**0x00000001**|  
|**DMTREEOP_SIBLINGS**|**0x00000002**|  
|**DMTREEOP_PARENT**|**0x00000004**|  
|**DMTREEOP_SELF**|**0x00000008**|  
|**DMTREEOP_DESCENDANTS**|**0x00000010**|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  