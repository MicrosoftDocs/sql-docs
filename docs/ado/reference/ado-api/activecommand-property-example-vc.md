---
title: "ActiveCommand Property Example (VC++) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "ActiveCommand property [ADO], VC++ example"
ms.assetid: 8269ea29-912a-4d20-9360-f48b3746081f
author: MightyPen
ms.author: genemi
manager: craigg
---
# ActiveCommand Property Example (VC++)
This example demonstrates the [ActiveCommand](../../../ado/reference/ado-api/activecommand-property-ado.md) property.  
  
 A subroutine is given a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object whose **ActiveCommand** property is used to display the command text and parameter that created the **Recordset**.  
  
## Example  
  
```  
// BeginActiveCommandCpp.cpp  
// compile with: /EHsc /c  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <ole2.h>  
#include <stdio.h>  
#include <conio.h>  
#include "icrsint.h"  
  
// Class extracts lname,fname from authors table.   
class CAuthorsRs : public CADORecordBinding {  
   BEGIN_ADO_BINDING(CAuthorsRs)  
  
   // Column lname is the 2nd field in the recordset  
   ADO_VARIABLE_LENGTH_ENTRY2(2, adVarChar, m_szau_lname,   
                              sizeof(m_szau_lname), lau_lnameStatus, TRUE)  
  
   // Column fname is the 3rd field in the recordset.  
   ADO_VARIABLE_LENGTH_ENTRY2(3, adVarChar, m_szau_fname,   
                              sizeof(m_szau_fname), lau_fnameStatus, TRUE)  
  
END_ADO_BINDING()  
  
public:  
   CHAR m_szau_fname[21];  
   ULONG lau_fnameStatus;      
   CHAR m_szau_lname[41];  
   ULONG lau_lnameStatus;  
};  
  
// Function declarations  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
void ActiveCommandX();  
void ActiveCommandXprint(_RecordsetPtr pRst);  
void PrintProviderError(_ConnectionPtr pConnection);  
void PrintComError(_com_error &e);  
  
inline char* mygets(char* strDest, int n) {  
   char strExBuff[10];  
   char* pstrRet = fgets(strDest, n, stdin);  
  
   if (pstrRet == NULL)  
      return NULL;  
  
   if (!strrchr(strDest, '\n'))  
      // Exhaust the input buffer.  
      do {  
         fgets(strExBuff, sizeof(strExBuff), stdin);  
      } while (!strrchr(strExBuff, '\n'));  
   else  
      // Replace '\n' with '\0'  
      strDest[strrchr(strDest, '\n') - strDest] = '\0';  
  
   return pstrRet;  
}  
  
int main() {  
   if (FAILED(::CoInitialize(NULL)))  
      return -1;  
  
   ActiveCommandX();  
   ::CoUninitialize();  
}  
  
void ActiveCommandX() {  
   HRESULT hr = S_OK;  
   char * token1;  
  
   // Define ADO object pointers, initialize pointers on define.  These are in the ADODB::  namespace.  
   _ConnectionPtr pConnection = NULL;  
   _CommandPtr pCmd = NULL;  
   _RecordsetPtr pRstAuthors = NULL;  
  
   // Definitions of other variables  
   _bstr_t strCnn("Provider='sqloledb'; Data Source='My_Data_Source'; Initial Catalog='pubs'; Integrated Security='SSPI';");  
   _bstr_t strPrompt;  
   _bstr_t strName;  
   CHAR strcharName[50];  
  
   try {  
      // Open connection.  
      TESTHR(pConnection.CreateInstance(__uuidof(Connection)));  
      TESTHR(pRstAuthors.CreateInstance(__uuidof(Recordset)));  
      TESTHR(pCmd.CreateInstance(__uuidof(Command)));  
  
      printf ("ActiveCommandX Example\n\n");  
      strPrompt = "Enter an author's name (e.g., Ringer): ";  
      printf(strPrompt);  
      mygets(strcharName, 50);  
      char *tempStr = strtok_s(strcharName, " \t", &token1);  
      strName = tempStr;  
  
      pCmd->CommandText = "SELECT * FROM authors WHERE au_lname = ?";  
      pCmd->Parameters->Append(pCmd->CreateParameter("LastName", adChar, adParamInput, 20, strName));  
  
      pConnection->Open (strCnn, "", "", adConnectUnspecified);  
      pCmd->PutActiveConnection(_variant_t((IDispatch*)pConnection));  
      pRstAuthors = pCmd->Execute(NULL, NULL, adCmdText);  
      ActiveCommandXprint(pRstAuthors);  
   }    // End Try statement.  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      // Pass a connection pointer accessed from the Recordset.  
      PrintProviderError(pConnection);  
      PrintComError(e);  
   }  
  
   // Clean up objects before exit.  
   if (pRstAuthors)  
      if (pRstAuthors->State == adStateOpen)  
         pRstAuthors->Close();  
   if (pConnection)  
      if (pConnection->State == adStateOpen)  
         pConnection->Close();  
}  
  
void ActiveCommandXprint(_RecordsetPtr pRst = NULL) {  
   // Varible Declaraion & initialization  
   IADORecordBinding *picRs = NULL;   // Interface Pointer declared.   
   CAuthorsRs autrs;   // C++ class object  
   _bstr_t strName;  
  
   // Open an IADORecordBinding interface pointer which   
   // we'll use for Binding Recordset to a class  
   TESTHR(pRst->QueryInterface(__uuidof(IADORecordBinding),(LPVOID*)&picRs));  
  
   // Bind the Recordset to a C++ Class here  
   TESTHR(picRs->BindToRecordset(&autrs));  
  
   strName = ((_CommandPtr)pRst->GetActiveCommand())->GetParameters()->GetItem("LastName")->Value;  
   printf("Command text = '%s'\n", (LPCSTR)((_CommandPtr)pRst->GetActiveCommand())->CommandText);  
   printf("Parameter = '%s'\n", (LPCSTR)strName);  
  
   if (pRst->BOF)  
      printf("Name = '%s'not found.", (LPCSTR)strName);  
   else {  
  
      printf ("Name = '%s  %s'",  
         autrs.lau_fnameStatus == adFldOK ? autrs.m_szau_fname : "<NULL>",  
         autrs.lau_lnameStatus == adFldOK ? autrs.m_szau_lname : "<NULL>");  
   }  
  
   // Release IADORecordset Interface      
   if (picRs)  
      picRs->Release();  
}  
  
void PrintProviderError(_ConnectionPtr pConnection) {  
   // Print Provider Errors from Connection object.  
   // pErr is a record object in the Connection's Error collection.  
   ErrorPtr pErr = NULL;  
   long nCount = 0;      
   long i = 0;  
  
   if ( (pConnection->Errors->Count) > 0) {  
      nCount = pConnection->Errors->Count;  
      // Collection ranges from 0 to (nCount - 1)  
      for ( i = 0 ; i < nCount ; i++ ) {  
         pErr = pConnection->Errors->GetItem(i);  
         printf("\t Error number: %x\t%s\n", pErr->Number, (LPCSTR)pErr->Description);  
      }  
   }  
}  
  
void PrintComError(_com_error &e) {  
   _bstr_t bstrSource(e.Source());  
   _bstr_t bstrDescription(e.Description());  
  
   // Print Com errors  
   printf("Error\n");  
   printf("\tCode = %08lx\n", e.Error());  
   printf("\tCode meaning = %s\n", e.ErrorMessage());  
   printf("\tSource = %s\n", (LPCSTR) bstrSource);  
   printf("\tDescription = %s\n", (LPCSTR) bstrDescription);  
}  
```  
  
## Sample Input  
  
```  
Ringer  
```  
  
## Sample Output  
  
```  
Command text = 'SELECT * FROM authors WHERE au_lname = ?'  
Parameter = 'Ringer'  
Name = 'Anne  Ringer'  
```  
  
## See Also  
 [ActiveCommand Property (ADO)](../../../ado/reference/ado-api/activecommand-property-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
