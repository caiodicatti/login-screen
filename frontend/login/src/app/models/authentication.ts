export interface authenticationResponse {
    success: boolean;
    statusCode: number;
    result: authenticationResult;
    message: string;
}

export interface authentication {
    Email: string;
    Senha: string;
}

export interface authenticationResult {
    idUsuario: number;
    nome: string;
    email: string;
}