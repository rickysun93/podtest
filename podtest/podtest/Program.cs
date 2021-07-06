using k8s;
using System;
using System.Linq;

namespace podtest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = KubernetesClientConfiguration.InClusterConfig();
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.ListNamespacedPod("default");
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
            }

            foreach (var item in list.Items.Where(i => i.Metadata.Name.StartsWith("azure-vote-back")))
            {
                client.DeleteNamespacedPod(item.Metadata.Name, "default");
            }

            if (list.Items.Count == 0)
            {
                Console.WriteLine("Empty!");
            }
        }
    }
}
