using Domain.Entities.Agendamento;
public static class AgendamentoFixture
{
    public static Agendamento CriarPadrao()
    {
        return new Agendamento(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow);
    }
}