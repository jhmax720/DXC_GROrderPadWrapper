using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace DXC_GROrderPadWrapper
{
    public class DXC_GROrderPadWrapper
    {

        //[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4)]
        //[StructLayoutAttribute(LayoutKind.Auto)]
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode/*, Pack = 1*/)]
        public struct OrderDllPars
        {
            [MarshalAs(UnmanagedType.LPWStr)] public string orderId;
            [MarshalAs(UnmanagedType.LPWStr)] public string companyName;
            [MarshalAs(UnmanagedType.LPWStr)] public string deliveryAddress1;
            [MarshalAs(UnmanagedType.LPWStr)] public string deliveryAddress2;
            [MarshalAs(UnmanagedType.LPWStr)] public string serverName;
            [MarshalAs(UnmanagedType.LPWStr)] public string databaseName;

        }

        [DllImport("GR_Orderpad.dll",
                    CharSet = CharSet.Unicode,         
                   CallingConvention = CallingConvention.StdCall
                   )]
       
        public static extern Boolean AXCreateNewOrder(
            [MarshalAs(UnmanagedType.LPWStr)] string serverName, 
            [MarshalAs(UnmanagedType.LPWStr)] string databaseName,
            [MarshalAs(UnmanagedType.LPWStr)] string partition,
            [MarshalAs(UnmanagedType.LPWStr)] string dataArea,
            [MarshalAs(UnmanagedType.LPWStr)] string orderId
            );
        [DllImport("GR_Orderpad.dll",
                    CharSet = CharSet.Unicode,
                   CallingConvention = CallingConvention.StdCall
                   )]
        public static extern Boolean AxEdtViewOrder([MarshalAs(UnmanagedType.LPWStr)] string serverName,
            [MarshalAs(UnmanagedType.LPWStr)] string databaseName,
            [MarshalAs(UnmanagedType.LPWStr)] string partition,
            [MarshalAs(UnmanagedType.LPWStr)] string dataArea,
            [MarshalAs(UnmanagedType.LPWStr)] string orderId,
            [MarshalAs(UnmanagedType.U1)] bool unknown);
        public string CreateNewOrder(string orderId, string serverName, string dbName, string partition, string dataArea) 
        {
            //OrderDllPars orderDllPars = new OrderDllPars();
            string ret;

            //orderDllPars.orderId        = orderId;
            //orderDllPars.companyName    = companyName;
            //orderDllPars.deliveryAddress1 = deliveryAddress1;
            //orderDllPars.deliveryAddress2 = deliveryAddress2;
            //orderDllPars.databaseName   = databaseName;
            //orderDllPars.serverName     = serverName;
            //orderDllPars.orderAmount = orderAmount;
            //orderDllPars.pSlipPath      = "ZZ";

            try
            {
                 var result = AXCreateNewOrder(serverName, dbName, partition, dataArea, orderId);

                ret = "OK";
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                ret = "Error: " + this.GetExceptionDetails(e);
            }

            return ret;
        }
        public string EditViewOrder(string orderId, string serverName, string dbName, string partition, string dataArea)
        {
            string ret;

            try
            {
                var result = AxEdtViewOrder(serverName, dbName, partition, dataArea, orderId, true);

                ret = "OK";
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                ret = "Error: " + this.GetExceptionDetails(e);
            }

            return ret;
        }
        public  string GetExceptionDetails(Exception exception)
        {
            var properties = exception.GetType()
                                    .GetProperties();
            var fields = properties
                             .Select(property => new
                             {
                                 Name = property.Name,
                                 Value = property.GetValue(exception, null)
                             })
                             .Select(x => String.Format(
                                 "{0} = {1}",
                                 x.Name,
                                 x.Value != null ? x.Value.ToString() : String.Empty
                             ));
            return String.Join("\n", fields);
        }
    }
}

