using System;
using EasyNetQ;
using Messages;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<TextMessage>("test", HandleTextMessage);

                /***
                 * Viene creata un'unica coda per ciascuna combinazione unica di tipo messaggio e subscription Id.
                 * Ciascuna chiamata a Subscribe() crea un nuovo Consumer.
                 * NOTA:
                 *   se si chiama il metodo Subscribe() 2 volte con lo stesso message type e lo stesso subscription Id (in questo caso ObjectMessage e "test")
                 *   si creano due Consumer che che consumeranno i messaggi dalla stessa coda. RabbitMQ pescherà i messaggi dalla coda e li inoltrerà
                 *   ai due consumer a turno (vedi esempio seguente)
                 ***/

                bus.Subscribe<ObjectMessage>("test", HandleObjectMessage);
                bus.Subscribe<ObjectMessage>("test", HandleObjectMessage2);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.WriteLine("Got message: {0}", textMessage.Text);
        }

        static void HandleObjectMessage(ObjectMessage objMessage)
        {
            Console.WriteLine("1° CONSUMER: Got message date: {0}", objMessage.OperationDate.ToLongDateString());
            Console.WriteLine("1° CONSUMER: Got message text: {0}", objMessage.Text);
        }
        static void HandleObjectMessage2(ObjectMessage objMessage)
        {
            Console.WriteLine("2° CONSUMER: Got message date: {0}", objMessage.OperationDate.ToLongDateString());
            Console.WriteLine("2° CONSUMER: Got message text: {0}", objMessage.Text);
        }        
    }
}