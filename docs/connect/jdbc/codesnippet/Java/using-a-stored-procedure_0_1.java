public static void executeUpdateStoredProcedure(Connection con) {
   try {
      CallableStatement cstmt = con.prepareCall("{call dbo.UpdateTestTable(?, ?)}");
      cstmt.setString(1, "A");
      cstmt.setInt(2, 100);
      cstmt.execute();
      int count = cstmt.getUpdateCount();
      cstmt.close();

      System.out.println("ROWS AFFECTED: " + count);
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}