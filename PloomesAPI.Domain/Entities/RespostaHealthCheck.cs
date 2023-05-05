using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace PloomesAPI.Domain.Entities
{
    public static class RespostaHealthCheck
    {
        public static string GerarRespostaHealthCheck(HealthReport resultadoHealthCheck, string nomeServicoPrincipal)
        {
            return JsonConvert.SerializeObject(new
            {
                Servico = nomeServicoPrincipal,
                Situacao = CustomizarSituacaoHealthCheck(resultadoHealthCheck.Status),
                Descricao = CustomizarDescricaoPrincipalHealthCheck(resultadoHealthCheck.Status),
                Dependencias = resultadoHealthCheck.Entries.Select((KeyValuePair<string, HealthReportEntry> e) => new
                {
                    Servico = e.Key,
                    Situacao = CustomizarSituacaoHealthCheck(e.Value.Status),
                    Descricao = e.Value.Description
                })
            });
        }

        private static string CustomizarDescricaoPrincipalHealthCheck(HealthStatus situacao)
        {
            string result = string.Empty;
            switch (situacao)
            {
                case HealthStatus.Unhealthy:
                    result = "Serviço possui dependências indisponíveis";
                    break;
                case HealthStatus.Degraded:
                    result = "Serviço possui dependências com avisos";
                    break;
                case HealthStatus.Healthy:
                    result = "Serviço operando normalmente";
                    break;
            }

            return result;
        }

        private static string CustomizarSituacaoHealthCheck(HealthStatus situacao)
        {
            string result = string.Empty;
            switch (situacao)
            {
                case HealthStatus.Unhealthy:
                    result = "Indísponivel";
                    break;
                case HealthStatus.Degraded:
                    result = "Aviso";
                    break;
                case HealthStatus.Healthy:
                    result = "Normal";
                    break;
            }

            return result;
        }
    }
}
