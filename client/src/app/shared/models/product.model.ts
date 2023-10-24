export interface ProductModel {
  id: string;
  name: string;
  description: string;
  priceInRubles: number;
  priceInDollars: number;
  generalNote: string;
  specialNote: string;
  categoryName: string;
  editing?: boolean;
}
