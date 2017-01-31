---
title: "Save and Open Methods Example (VJ++) | Microsoft Docs"
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
  - "Save method [ADO], VJ++ example"
  - "Open method [ADO]"
ms.assetid: bc425816-ecf8-4739-b50e-4cd5c60a151c
caps.latest.revision: 11
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Save and Open Methods Example (VJ++)
These three examples demonstrate how the [Save](../../../ado/reference/ado-api/save-method.md) and **Open** methods can be used together.  
  
 Assume that you are going on a business trip and want to take along a table from a database. Before you go, you access the data as a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) and save it in a transportable form. When you arrive at your destination, you access the **Recordset** as a local, disconnected **Recordset**. You make changes to the **Recordset**, and then save it again, along with your changes. Finally, when you return home, you connect to the database again and update it with the changes you made on the road.  
  
```  
// BeginSaveJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class SaveX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      SaveX1();  
      SaveX2();  
      SaveX3();  
      System.exit(0);  
   }  
  
   // First, access and save the Authors table.  
  
   // SaveX1 function  
  
   static void SaveX1()  
   {  
      // Define ADO Objects.  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
                      "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
      File file;  
  
      try  
      {  
         rstAuthors = new Recordset();  
         rstAuthors.setCursorLocation(AdoEnums.CursorLocation.CLIENT);  
         rstAuthors.open("SELECT * FROM Authors",  
                        strCnn,  
                        AdoEnums.CursorType.DYNAMIC,  
                        AdoEnums.LockType.OPTIMISTIC,  
                        AdoEnums.CommandType.TEXT);  
  
         // For the sake of illustration, save the recordset to a   
         // disk in XML format.  
         file = new File("c:\\Pubs.xml");  
         if(!file.exists())  
            rstAuthors.save("c:\\Pubs.xml",AdoEnums.PersistFormat.XML);  
         else  
         {  
            System.out.println("\nFile already exists.");  
            System.out.println("\nPress <Enter> to continue..");  
            in.readLine();  
            System.exit(0);  
         }  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
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
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();  
      }  
   }  
  
   // At this point, you have arrived at your destination. You will  
   // access the Authors table as a local, disconnected Recordset.  
   // Don't forget you must have the MSPersist provider on the machine  
   // you are using in order to access the saved file, a:\Pubs.xml.  
  
   // SaveX2 function  
  
   static void SaveX2()  
   {  
      // Define ADO Objects.  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         rstAuthors = new Recordset();  
  
         // For sake of illustration, we specify all parameters.  
         rstAuthors.open("c:\\Pubs.xml",  
               "Provider=MSPersist;",  
               AdoEnums.CursorType.FORWARDONLY,  
               AdoEnums.LockType.BATCHOPTIMISTIC,  
               AdoEnums.CommandType.FILE);  
  
         // Now you have a local, disconnected recordset.  
         // Edit it however you want.  
         // (In this example, the change makes no difference).  
         rstAuthors.find("au_lname = 'Carson'");  
         if(rstAuthors.getEOF())  
         {  
            System.out.println("Name not found.");  
            System.out.println("\nPress <Enter> to continue..");  
            in.readLine();  
            return;  
         }  
         rstAuthors.getField("city").setString("Berkeley");  
         rstAuthors.update();  
  
         // Save changes in ADTG format this time, for illustration.  
         // Note that previous version on the disk, as a:\Pubs.xml.  
         rstAuthors.save("c:\\Pubs.adtg",AdoEnums.PersistFormat.ADTG);  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
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
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();  
      }  
   }  
  
   // Finally, update the database with your changes.  
  
   // SaveX3 function  
  
   static void SaveX3()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +   
                      "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      try  
      {  
         // If there is no ActiveConnection, you can open with defaults.  
         rstAuthors = new Recordset();  
         rstAuthors.open("c:\\Pubs.adtg");  
  
         // Connect to the database, associate the Recordset with  
         // connection, then update the database table with the changed  
         // Recordset.  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
         rstAuthors.setActiveConnection(cnConn1);  
         rstAuthors.updateBatch();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }   
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
      }  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
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
// EndSaveJ  
  
```