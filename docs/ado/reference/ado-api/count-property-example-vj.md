---
title: "Count Property Example (VJ++) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Count property [ADO], VJ++ example"
ms.assetid: 68cc1395-2433-4000-98dc-9e860170cd59
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Count Property Example (VJ++)
This example demonstrates the [Count](../../../ado/reference/ado-api/count-property-ado.md) property with two collections in the ***Employees*** database. The property obtains the number of objects in each collection, and sets the upper limit for loops that enumerate these collections. Another way to enumerate these collections without using the **Count** property would be to use `For Each...Next` statements.  
  
```  
// BeginCountJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class CountX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      CountX();  
      System.exit(0);  
   }  
  
   // CountX function  
  
   static void CountX()  
   {  
  
      // Define ADO Objects.  
      Recordset rstEmployees = null;  
  
      // Declarations.  
      BufferedReader in =  
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      int intLoop;  
      int intDisplaySize = 20;  
      int recCount=0;  
  
      try  
      {  
         rstEmployees = new Recordset();  
  
         // Open recordset with data from Employees table.  
         rstEmployees.open("employee", strCnn,  
            AdoEnums.CursorType.FORWARDONLY,  
            AdoEnums.LockType.READONLY,  
            AdoEnums.CommandType.TABLE);  
  
         // Print information about Fields collection.  
         System.out.println(rstEmployees.getFields().getCount() +  
            " Fields in Employees");  
         for ( intLoop = 0; intLoop <  
            rstEmployees.getFields().getCount(); intLoop++)  
         {  
            System.out.println("\t" +  
               rstEmployees.getFields().getItem(intLoop).getName());  
         }  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
  
         // Print information about Properties collection.  
         System.out.println(rstEmployees.getProperties().getCount() +  
            " Properties in Employees");  
         for ( intLoop = 0; intLoop <  
            rstEmployees.getProperties().getCount(); intLoop++)  
         {  
            System.out.println("\t" +  
               rstEmployees.getProperties().getItem(intLoop).getName());  
            recCount++;  
            if ( recCount >= intDisplaySize)  
            {  
               System.out.println("\n\nPress <Enter> to continue..");  
               in.readLine();  
               recCount = 0;  
            }  
         }  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if (rstEmployees.getActiveConnection()==null)  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
         else  
         {  
            // As passing a Recordset, check for null pointer first.  
            if (rstEmployees != null)  
            {  
               PrintProviderError(rstEmployees.getActiveConnection());  
            }  
            else  
            {  
               System.out.println("Exception: " + ae.getMessage());  
            }  
         }  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstEmployees != null)  
            if (rstEmployees.getState() == 1)  
               rstEmployees.close();  
      }  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connection's Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
  
// EndCountJ  
```  
  
## See Also  
 [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)