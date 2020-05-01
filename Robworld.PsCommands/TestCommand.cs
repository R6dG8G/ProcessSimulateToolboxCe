using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecnomatix.Engineering;


namespace Robworld.PsCommands
{
    public class TestCommand : TxButtonCommand
    {
        public override string Category { get { return "Test"; } }

        public override string Name { get { return "Test"; } }

        public override void Execute(object cmdParams)
        {
            
        }
    }
}
