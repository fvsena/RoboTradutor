using RoboTradutor.Models;

namespace RoboTradutor.Processos
{
    interface IProcesso
    {
        Resultado Iniciar();
        void Erro();
        void Successo();
    }
}
