export interface registerResponse {
    success: boolean;
    statusCode: number;
    result: register;
    message: string;
}

export interface register {
    nome: string;
    email: string;
    senha: string;
}
