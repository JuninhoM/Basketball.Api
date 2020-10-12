namespace Basketball.Model.ViewModel
{
    public class ResponseViewModel : ResponseViewModel<dynamic>
    {
        public ResponseViewModel() { }

        public ResponseViewModel(dynamic content, bool success, string feedback)
        {
            Content = content;
            Success = success;
            Feedback = feedback;
        }
    }

    public class ResponseViewModel<T>
    {
        public ResponseViewModel() { }

        public ResponseViewModel(T content, bool success, string feedback)
        {
            Content = content;
            Success = success;
            Feedback = feedback;
        }

        // Retorno do método, com tipo customizável
        public T Content { get; set; }

        // Variável de controle de comunicação com a API (false apenas para erro técnico/catch dos métodos)
        public bool Success { get; set; }

        // Mensagem de retorno
        public string Feedback { get; set; }
    }
}
