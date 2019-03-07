---
title: "Parameter (ADO - WFC Syntax) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "02/15/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Parameter collection [ADO], ADO/WFC syntax"
ms.assetid: d00d1e1e-14b1-41a2-a00f-2a3cb7396f15
author: MightyPen
ms.author: genemi
manager: craigg
---
# Parameter (ADO - WFC Syntax)
## package com.ms.wfc.data  
  
### Constructor  
  
```  
public Parameter()  
public Parameter(String name)  
public Parameter(String name, int type)  
public Parameter(String name, int type, int dir)  
public Parameter(String name, int type, int dir, int size)  
public Parameter(String name, int type, int dir, int size, Object value)  
```  
  
### Methods  
  
```  
public void appendChunk(byte[] bytes)  
public void appendChunk(char[] chars)  
public void appendChunk(String chars)  
```  
  
### Properties  
  
```  
public int getAttributes()  
public void setAttributes(int attr)  
public int getDirection()  
public void setDirection(int dir)  
public String getName()  
public void setName(String name)  
public int getNumericScale()  
public void setNumericScale(int scale)  
public int getPrecision()  
public void setPrecision(int prec)  
public int getSize()  
public void setSize(int size)  
public int getType()  
public void setType(int type)  
public com.ms.com.Variant getValue()  
public void setValue(Object v)  
public AdoProperties getProperties()  
```  
  
## Parameter Accessor Methods  
 The [Value](../../../ado/reference/ado-api/value-property-ado.md) property of a [Parameter](../../../ado/reference/ado-api/parameter-object.md) object gets or sets the content of that object. The content is represented as a VARIANT, a type of object that can be assigned a value and any of several data types.  
  
 ADO/WFC implements the **Value** property with the **getValue** method, which returns a VARIANT object; and the **setValue** method, which takes a VARIANT as an argument. VARIANTs are highly efficient in certain languages, such as Microsoft Visual Basic.  
  
 In addition to the **Value** property, ADO/WFC provides *accessor* methods that use Java data types to get and set the content of **Parameter** objects. Most of these methods have names of the form **get**_DataType_ or **set**_DataType_.  
  
 There is one noteworthy exception: There is no **getNull** property; instead, there is an **isNull** property that returns a Boolean value indicating whether the field is null.  
  
```  
public boolean getBoolean()  
public void setBoolean(boolean v)  
public byte getByte()  
public void setByte(byte v)  
public double getDouble()  
public void setDouble(double v)  
public float getFloat()  
public void setFloat(float v)  
public int getInt()  
public void setInt(int v)  
public long getLong()  
public void setLong(long v)  
public short getShort()  
public void setShort(short v)  
public String getString()  
public void setString(String v)  
public boolean isNull()  
public void setNull()  
```  
  
## See Also  
 [Parameter Object](../../../ado/reference/ado-api/parameter-object.md)
