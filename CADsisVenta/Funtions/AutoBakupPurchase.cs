using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CADsisVenta.Funtions
{
    public class AutoBakupPurchase
    {
        private CancellationTokenSource _cancelTokenSource;
        public Action<CancellationToken> ActionToExecute;

        public AutoBakupPurchase(CancellationTokenSource cancelationToken)
        {

            _cancelTokenSource = cancelationToken;
        }



        public void Star()
        {

            try
            {
                Task.Factory.StartNew(() => ActionToExecute(_cancelTokenSource.Token))
                    .ContinueWith(complet => TaskCompleted(), _cancelTokenSource.Token);

            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

        }


        private void TaskCompleted()
        {
            releaseCancellationTokenSource();
        }

        private void releaseCancellationTokenSource()
        {
            if (_cancelTokenSource != null)
            {
                _cancelTokenSource.Dispose();
                _cancelTokenSource = null;
            }

        }

        public void StarBackUp(List<IDictionary<String, Double>> data)
        {
            foreach (var item in data)
            {


            }
        }
    }
}
