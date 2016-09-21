using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Tracer
{

    public class MethodTree
    {
        public MethodNode Root { get; }
        public MethodNode LastMethod { get; set; }

        public MethodTree()
        {
            this.Root = new MethodNode();
            this.LastMethod = Root;
        }

        public IEnumerable<MethodNode> BypassNode(MethodNode node)
        {
            if (node != null)
            {
                yield return node;
                foreach (MethodNode methodNode in node.ChildrenList)
                { 
                    foreach (MethodNode methodNodeNode in BypassNode(methodNode))
                        yield return methodNodeNode;
                }
            }
            else
            {
                yield break;
            }
        }


        public IEnumerable<MethodNode> BypassTree()
        {
            foreach (MethodNode node in Root.ChildrenList)
            {
                foreach (MethodNode methodNode in BypassNode(node))
                {
                    yield return methodNode;
                }
            }
        }
    }
}
