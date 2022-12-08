---
description: "VBA functions in MDX and DAX"
title: "VBA functions in MDX and DAX | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# VBA functions in MDX and DAX


  This document contains a crossed reference of all VBA functions available in [Visual Basic for Applications Functions](/office/vba/Language/Reference/functions-visual-basic-for-applications) that are supported in MDX. The list includes notes when there is functional equivalence with the DAX language.  
  
## Visual Basic for Applications Functions Reference  
  
|Function Name|Supported|Notes|  
|-------------------|---------------|-----------|  
|Abs|DAX, MDX||  
|Array|Not supported||  
|Asc|MDX only||  
|AscW|MDX only||  
|Atn|MDX only||  
|CallByName|Not supported||  
|CBool|MDX only||  
|CByte|MDX only||  
|CCur|MDX only||  
|CDate|MDX only||  
|CDbl|MDX only||  
|CDec|MDX only||  
|Choose|MDX only||  
|Chr|MDX only||  
|CInt|MDX only||  
|CLng|MDX only||  
|CLngLng|Not supported||  
|CLngPtr|Not supported||  
|Command|Not supported||  
|Cos|MDX only||  
|CreateObject|Not supported||  
|CSng|MDX only||  
|CStr|MDX only||  
|CurDir|Not supported||  
|CVar|MDX only||  
|CVErr|Not supported||  
|Date|MDX only|**Warning** DAX implements a different function with the same name; the DATE(Year, Month, Day) function, used to generate a date type value from the given arguments|  
|DateAdd|MDX only|**Warning** DAX implements a different function with the same name; the DATEADD (\<dates>,<number_of_intervals>,\<interval>) function, used to shift the given dates by a number of given intervals|  
|DateDiff|MDX only||  
|DatePart|MDX only||  
|DateSerial|MDX only||  
|DateValue|DAX, MDX||  
|Day|DAX, MDX||  
|DDB|MDX only||  
|Dir|Not supported||  
|DoEvents|Not supported||  
|Environ|Not supported||  
|EOF|Not supported||  
|Error|Not supported||  
|Exp|DAX, MDX||  
|FileAttr|Not supported||  
|FileDateTime|Not supported||  
|FileLen|Not supported||  
|Filter|Not supported|**Warning** MDX implements a different function with the same name; the FILTER(Set_Expression, Logical_Expression) function returns the set that results from filtering a specified set based on a search condition from the given arguments<br /><br /> **Warning** DAX implements a different function with the same name; the FILTER (\<table>,\<filter>) function Returns a table that represents a subset of another table or expression from the given arguments|  
|Fix|MDX only||  
|Format  (Visual Basic for Applications)|DAX, MDX||  
|FormatCurrency|Not supported||  
|FormatDateTime|Not supported||  
|FormatNumber|Not supported||  
|FormatPercent|Not supported||  
|FreeFile|Not supported||  
|FV|MDX only||  
|GetAllSettings|Not supported||  
|GetAttr|Not supported||  
|GetObject|Not supported||  
|GetSetting|Not supported||  
|Hex|MDX only||  
|Hour|DAX, MDX||  
|Iif|MDX only|**Warning** DAX implements a similar function with the name: IF (logical_test, value_if_true, value_if_false) function.|  
|IMEStatus|Not supported||  
|Input|Not supported||  
|InputBox|Not supported||  
|InStr|MDX only||  
|InStrRev|Not supported||  
|Int|DAX, MDX||  
|IPmt|MDX only||  
|IRR|MDX only||  
|IsArray|MDX only||  
|IsDateMDX only||  
|IsEmpty|MDX only||  
|IsError|DAX, MDX||  
|IsMissing|MDX only||  
|IsNull|MDX only||  
|IsNumeric|MDX only||  
|IsObject|Not supported||  
|Join|Not supported||  
|LBound|Not supported||  
|LCase|MDX only||  
|Left|DAX, MDX||  
|Len|DAX, MDX||  
|Loc|Not supported||  
|LOF|Not supported||  
|Log|MDX only|**Important** DAX implements a different function with the same name; the LOG (number, base) function. Returns the logarithm of a number to the base specified from the given arguments.|  
|LTrim|MDX only||  
|MacID|Not supported||  
|MacScript|Not supported||  
|Mid|DAX, MDX||  
|Minute|DAX, MDX||  
|MIRR|MDX only||  
|Month|DAX, MDX||  
|MonthName|Not supported||  
|MsgBox|Not supported||  
|Now|DAX, MDX||  
|NPer|MDX only||  
|NPV|MDX only||  
|Oct|MDX only||  
|Partition|MDX only||  
|Pmt|MDX only||  
|PPmt|MDX only||  
|PV|MDX only||  
|QBColor|MDX only||  
|Rate|MDX only||  
|Replace|Not supported||  
|RGB|MDX only||  
|Right|DAX, MDX||  
|Rnd|MDX only||  
|Round|DAX, MDX||  
|RTrim|MDX only||  
|Second|DAX, MDX||  
|Seek|Not supported||  
|Sgn|DAX, MDX||  
|Shell|Not supported||  
|Sin|MDX only||  
|SLN|MDX only||  
|Space|MDX only||  
|Spc|Not supported||  
|Split|Not supported||  
|Sqr|MDX only||  
|Str|MDX only||  
|StrComp|MDX only||  
|StrConv|MDX only||  
|String|MDX only||  
|StrReverse|Not supported||  
|Switch|MDX only||  
|SYD|MDX only||  
|Tab|Not supported||  
|Tan|MDX only||  
|Time|Not supported||  
|Timer|MDX only||  
|TimeSerial|MDX only||  
|TimeValue|DAX, MDX||  
|Trim|DAX, MDX||  
|TypeName|MDX only||  
|UBound|Not supported||  
|UCase|MDX only||  
|Val|MDX only||  
|VarType|Not supported||  
|Weekday|DAX, MDX||  
|WeekdayName|Not supported||  
|Year|DAX, MDX||  
  
