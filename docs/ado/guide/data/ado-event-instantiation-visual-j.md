---
title: "ADO Event Instantiation: Visual J++ | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e22eedae-07d5-4b1b-a447-4151d1a6c098
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# ADO Event Instantiation: Visual J++
This short Microsoft® Visual J++® example shows how you can associate your own function with a particular event.  
  
```  
// BeginEventExampleVJ  
import wfc.data.*;  
public class MyClass  
{  
ConnectionEventHandler handler =   
    new ConnectionEventHandler(this,"onConnectComplete");  
  
    public void onConnectComplete(Object sender,ConnectionEvent e)  
    {  
        if (e.adStatus == AdoEnums.EventStatus.ERRORSOCCURRED)   
            System.out.println("Connection failed");  
        else  
            System.out.println("Connection completed");  
        return;  
    }  
  
    void main( void )  
    {  
        Connection conn = new Connection();  
  
        conn.addOnConnectComplete(handler);     // Enable event support.  
        conn.open("DSN=Pubs");  
        conn.close();  
        conn.removeOnConnectComplete(handler);  // Disable event support.  
    }  
}  
// EndEventExampleVJ  
```  
  
 First, the class method *onConnectionComplete* is associated with the **ConnectionComplete** event by creating a new **ConnectionEventHandler** object and assigning the *onConnectComplete* function to the object.  
  
 The *main* function then creates a **Connection** object and enables event handling by calling the **addOnConnectComplete** method and passing it the address of the *handler* function.