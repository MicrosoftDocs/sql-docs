---
title: "Version Property Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Version property [ADO], VJ++ example"
ms.assetid: 40b2ab58-84b5-4ae6-9226-df9e8f7d97c6
caps.latest.revision: 11
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# Version Property Example (VJ++)
This example uses the [Version](../../../ado/reference/ado-api/version-property-ado.md) property of a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object to display the current ADO version. It also uses several dynamic properties to show:  
  
-   Current DBMS name and version.  
  
-   OLE DB version.  
  
-   Provider name and version.  
  
-   ODBC version.  
  
-   ODBC driver name and version.  
  
```  
// BeginVersionJ  
import com.ms.wfc.data.*;  
import java.io.*;  
  
public class VersionX  
{  
   // The main entry point of the application.  
   public static void main (String[] args)  
   {  
      VersionX();  
      System.exit(0);  
   }  
   // VersionX Function  
   static void VersionX()  
   {  
      // Define ADO Objects.  
      Connection cnn1 = null;  
  
      // Declarations.  
      String strCnn = "Driver={SQL Server};Server='MySqlServer';" +  
                  "User ID='MyUserID';Password='MyPassword';database='Pubs';";  
      String strVersionInfo;  
      BufferedReader in = new   
         BufferedReader(new InputStreamReader(System.in));  
      try  
      {  
         // Open connection.  
         cnn1 = new Connection();  
         cnn1.open(strCnn);  
  
         strVersionInfo = "\tADO Version:\t\t" +   
            cnn1.getVersion().toString()+"\n"+  
            "\tDBMS Name:\t\t" +   
            cnn1.getProperties().getItem("DBMS Name").getString() +"\n"+  
            "\tDBMS Version:\t\t"+   
            cnn1.getProperties().getItem("DBMS Version").getString() +   
            "\n" + "\tOLE DB Version:\t\t" +   
            cnn1.getProperties().getItem("OLE DB Version").getString()+   
            "\n" + "\tProvider Name:\t\t" +   
            cnn1.getProperties().getItem("Provider Name").getString() +   
            "\n" + "\tProvider Version:\t" +   
            cnn1.getProperties().getItem("Provider Version").  
            getString() + "\n" + "\tDriver Name:\t\t" +   
            cnn1.getProperties().getItem("Driver Name").getString() +   
            "\n" + "\tDriver Version:\t\t" +   
            cnn1.getProperties().getItem("Driver Version").getString()+   
            "\n" + "\tDriver ODBC Version:\t" +   
            cnn1.getProperties().getItem(  
            "Driver ODBC Version").getString()+ "\n";  
         System.out.println("\n\n" + strVersionInfo);  
  
         System.out.println("Press <Enter> to continue..");  
         in.readLine();  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
      {  
         PrintIOError(je);  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a recordset, check for null pointer first.  
         if(cnn1!= null)  
         {  
            PrintProviderError(cnn1);  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
      finally  
      {  
         // Cleanup objects before exit.     
         if (cnn1 != null)  
            if (cnn1.getState() == 1)  
               cnn1.close();  
      }  
  
   }  
   // PrintProviderError Function  
   static void PrintProviderError(Connection cnn1)  
   {  
      // Print Provider Errors from Connection Object.  
      // ErrItem is an item object in the Connections Errors Collection.  
      com.ms.wfc.data.Error               ErrItem = null;  
      long                                 nCount = 0;  
      int                                       i = 0;  
  
      nCount = cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if ( nCount > 0)  
      {  
         // Collection ranges from 0 to nCount-1.  
         for ( i=0;i<nCount; i++)  
         {  
            ErrItem = cnn1.getErrors().getItem(i);  
            System.out.println("\t Error Number: " + ErrItem.getNumber()   
               + "\t" + ErrItem.getDescription());  
         }  
      }  
   }  
   // PrintIOError Function  
   static void PrintIOError(java.io.IOException je)  
   {  
      System.out.println("Error: \n");  
      System.out.println("\t Source: " + je.getClass() + "\n");  
      System.out.println("\t Description: "+ je.getMessage() + "\n");  
   }  
}  
// EndVersionJ  
  
```  
  
## See Also  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Version Property (ADO)](../../../ado/reference/ado-api/version-property-ado.md)