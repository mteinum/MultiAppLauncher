using System;
using System.Linq;
using System.Threading;

namespace MultiAppLauncher
{
    public sealed class CpuController : IDisposable
    {
        private DateTime _lastCpuMeasureTime;
        private readonly Thread _cpuMeasureThread;
        private readonly AutoResetEvent _exitEvent;
        private readonly IMainFormView _view;

        public CpuController(IMainFormView view)
        {
            _view = view;

            _exitEvent = new AutoResetEvent(false);
            _cpuMeasureThread = new Thread(MeasureCpu);
            _cpuMeasureThread.Start();
        }

        public void Stop()
        {
            _exitEvent.Set();
            _cpuMeasureThread.Join();
        }

        private void MeasureCpu()
        {
            _lastCpuMeasureTime = DateTime.Now;

            while (true)
            {
                if (_exitEvent.WaitOne(TimeSpan.FromMilliseconds(500)))
                    break;

                CheckCpu();
            }
        }

        private void CheckCpu()
        {
            double duration = DateTime.Now.Subtract(_lastCpuMeasureTime).TotalMilliseconds;
            _lastCpuMeasureTime = DateTime.Now;

            var processes = _view.GetListViewItems()
                .Where(t => t.Tag != null)
                .ToList();

            foreach (var lvi in processes)
            {
                var holder = lvi.GetProcessHolder();

                if (holder == null)
                    continue;

                var oldCpu = holder.CpuUsage;
                var newCpu = holder.Process.TotalProcessorTime.TotalMilliseconds;

                holder.CpuUsage = newCpu;

                var cpu = (newCpu - oldCpu) / duration / Environment.ProcessorCount;

                _view.SetListViewItem(lvi, Columns.Cpu, cpu.ToString("P"));
            }
        }

        public void Dispose()
        {
            _exitEvent.Dispose();
        }
    }
}