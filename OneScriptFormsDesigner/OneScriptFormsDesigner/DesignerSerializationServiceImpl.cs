﻿using System;
using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;
using System.Collections;

namespace osfDesigner
{
    internal class DesignerSerializationServiceImpl : IDesignerSerializationService
    {
        private IServiceProvider _serviceProvider;

        public DesignerSerializationServiceImpl ( IServiceProvider serviceProvider )
        {
            this._serviceProvider = serviceProvider;
        }

        public System.Collections.ICollection Deserialize ( object serializationData )
        {
            SerializationStore serializationStore = serializationData as SerializationStore;
            if ( serializationStore != null )
            {
                ComponentSerializationService componentSerializationService = _serviceProvider.GetService ( typeof ( ComponentSerializationService ) ) as ComponentSerializationService;
                ICollection collection = componentSerializationService.Deserialize ( serializationStore );
                return collection;
            }
            return new object[] {};
        }

        public object Serialize ( System.Collections.ICollection objects )
        {
            ComponentSerializationService componentSerializationService = _serviceProvider.GetService ( typeof ( ComponentSerializationService ) ) as ComponentSerializationService;
            SerializationStore returnObject = null;
            using (SerializationStore serializationStore = componentSerializationService.CreateStore())
            {
                foreach (object obj in objects)
                {
                    if (obj is Control)
                    {
                        componentSerializationService.Serialize(serializationStore, obj);
                    }
                }
                returnObject = serializationStore;
            }
            return returnObject;
        }
    }
}
